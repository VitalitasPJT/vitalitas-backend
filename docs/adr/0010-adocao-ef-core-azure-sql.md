# 0010 — Adoção do EF Core Code-First para schema/migrations no Azure SQL

## Status

Implementado e validado — a migration `InitialCreate` foi aplicada com sucesso na instância real do Azure SQL (`server-sql-vitalitas` / `sql-db-vitalitas`) em 2026-08-07.

## Contexto

O time centralizou o banco de dados do Vitalitas em uma instância única do Azure SQL, antes desta mudança vazia (sem schema aplicado). Até esta mudança, o acesso a dados do backend era 100% via Dapper com SQL cru, escrito diretamente nos repositories (`Infrastructure/Repositories/**`), sem nenhuma ferramenta de versionamento de schema — a criação/alteração de tabelas era manual.

Com um banco centralizado compartilhado por todo o time (em vez de um SQL Server local por desenvolvedor), esse modelo manual se torna arriscado: não há histórico versionado de como o schema chegou ao estado atual, nem um jeito automatizado e repetível de aplicar a mesma alteração de schema em todos os ambientes (local de cada dev, e futuramente CI/produção).

## Decisão

Adotado o EF Core (Code-First/migrations) **apenas como ferramenta de versionamento e aplicação de schema**, não como mecanismo de acesso a dados em runtime:

- `AppDbContext` (`src/Infrastructure/Database/Context/AppDbContext.cs`) mapeia, via Fluent API (`IEntityTypeConfiguration<T>` em `src/Infrastructure/Database/Configurations/**`), todas as entidades já existentes em `src/Domain/Features/**` — 21 tabelas ao todo.
- As colunas/tabelas foram nomeadas para bater com o schema que os repositories Dapper já esperavam (ex.: tabela `Usuario` com colunas `IdUsuario`, `Nome`, `CPF`, etc. — o Azure SQL usa collation case-insensitive por padrão, então `IdUsuario` gerado pelo EF e `idUsuario` usado nas queries Dapper resolvem para a mesma coluna).
- **Os repositories Dapper existentes não foram alterados** e continuam sendo o único caminho de leitura/escrita em runtime. O `AppDbContext` só é instanciado para gerar (`dotnet ef migrations add`) e aplicar (`dotnet ef database update`) migrations.
- Não foram declaradas relações EF (`HasOne`/`HasMany`) entre entidades: o modelo de domínio atual não expõe navigation properties (é o mesmo modelo anêmico consumido pelos repositories Dapper), então as colunas de referência (`IdUsuario`, `IdAcademia`, etc.) permanecem como colunas escalares simples, sem FK declarada via EF — igual ao que já valia implicitamente no acesso via Dapper. Isso também evita lidar com a dependência circular real que existe entre `Usuario` (tem `IdAcademia`) e `Academia` (tem `IdGestor`, que aponta para um `Usuario`).
- Todos os IDs (`Guid`) são gerados em código (`Guid.NewGuid()` nos construtores das entidades, como já acontecia), então as colunas de chave primária foram configuradas com `ValueGeneratedNever()` — o EF não deve gerar nem esperar um valor default do banco para elas. A exceção é `Agenda.IdAgenda` (`int`), que já nascia com placeholder `0` no construtor, indicando identity/auto-incremento — mantido como `ValueGeneratedOnAdd()`.
- Dois value objects (`CNPJ` e `Monetario`, em `src/Domain/ValueObjects`) guardavam o valor em campo privado sem nenhum acessor público, ao contrário dos irmãos (`CPF`, `CREF`, `Nome`, que já expunham `Valor`). Foi adicionado `public string Valor { get; private set; }` a ambos, seguindo o padrão já existente — sem essa propriedade não havia como o EF (nem qualquer código externo) ler o valor para persistir a coluna.

## Alternativas consideradas

- **EF Core como substituto do Dapper** — descartada por ora. Trocar o mecanismo de acesso a dados é uma mudança de escopo muito maior, que afeta todos os repositories já implementados e testados manualmente; o problema imediato a resolver era só "como versionar e aplicar schema num banco centralizado vazio", não "qual ORM usar". Fica como decisão futura separada se o time optar por migrar os repositories.
- **Scripts SQL manuais versionados em pasta `sql/migrations`** — resolveria o versionamento, mas exigiria escrever `CREATE TABLE` para as ~21 entidades à mão e manter esses scripts sincronizados manualmente com as classes de domínio a cada mudança. O Code-First gera isso a partir do próprio modelo já existente.
- **Declarar relações EF completas (FKs, cascade delete, navigation properties)** — descartada nesta rodada porque exigiria adicionar navigation properties às entidades de domínio (mudança de modelo, não só de infraestrutura) e resolver a dependência circular Usuario↔Academia. Fica como possível ADR futuro se o time quiser integridade referencial real no banco.

## Consequências

- O Azure SQL vazio agora tem um caminho repetível para receber o schema inicial: `dotnet ef database update` (ver README, seção "Configuração Inicial (Devs)").
- Qualquer alteração futura em uma entidade de domínio (`src/Domain/Features/**`) precisa de uma migration nova (`dotnet ef migrations add NomeDaMudanca`) para o schema do Azure SQL acompanhar — isso é responsabilidade de quem alterar a entidade, e deve ser comunicado ao time antes do push (ver AVISO CRÍTICO DE WORKFLOW no README, já que o banco é compartilhado).
- Como não há FKs nem integridade referencial declaradas via EF, o EF Core **não impede** inconsistências entre tabelas — essa responsabilidade continua 100% nos repositories Dapper, como já era.
- Identificado um descompasso pré-existente, não introduzido por esta mudança: `GestorRepository.CriarFuncionario` grava `Cargo` como texto (`cargo.ToString()`), enquanto o restante do sistema trata enums como `int` (ex.: `TipoUsuario`). A migration inicial mapeia `Cargo` como `int`, consistente com o resto do schema; alinhar o repository fica como item separado, fora do escopo desta mudança.
- `dotnet-ef` (versão 9.0.18) foi adicionado como tool local via `.config/dotnet-tools.json` — cada desenvolvedor/pipeline precisa rodar `dotnet tool restore` uma vez antes de usar comandos `dotnet ef`.
- Descoberto durante o rollout real: máquinas sem o runtime .NET 9 instalado (só 8.x/10.x) fazem `dotnet ef` falhar com "You must install or update .NET to run this application" — a correção é `DOTNET_ROLL_FORWARD=LatestMajor` (por sessão de terminal), **não** fazer downgrade do `dotnet-ef`/pacotes EF Core para 8.x, o que diverge do `Directory.Packages.props` e gera conflito de versão (`NU1605`) no próximo `dotnet restore`. Documentado na seção "Troubleshooting" do README.
- Conexão ao Azure SQL exige o IP do desenvolvedor liberado no firewall do servidor (Portal do Azure → servidor SQL → Segurança/Rede) — sem isso, `dotnet ef database update` e `dotnet run` falham por timeout, não por erro de configuração da aplicação. Documentado no README ("0. Liberar seu IP no firewall do Azure SQL").
