# 0014 — Migração de Dapper (SQL manual) para Entity Framework Core

## Status

Implementado.

## Contexto

Até esta sessão, o acesso a dados era feito via Dapper + `Microsoft.Data.SqlClient`: 5 repositórios (`UsuarioRepository`, `AlunoRepository`, `GestorRepository`, `FichaMedicaRepository`, `RefreshTokenRepository`) rodavam SQL cru contra uma conexão criada por `DbConnectionFactory`. O schema do banco vivia em dois scripts soltos (`CREATE.sql`, `INSERT.sql`) que precisavam ser rodados manualmente no SSMS/`sqlcmd` a cada ambiente novo — sem versionamento de schema, sem histórico de migrações, sem forma automática de saber se o banco de um ambiente estava desatualizado em relação ao código.

Duas coisas forçaram essa mudança:

1. Pedido explícito do usuário para parar de rodar SQL manualmente e passar a usar EF Core Code-First com Migrations a partir de agora.
2. A implementação de Multi-Tenancy (ADR 0013) depende de `HasQueryFilter`, um mecanismo específico do EF Core sem equivalente limpo em Dapper. Se os 5 repositórios continuassem em Dapper, o filtro de tenant "existiria" no código mas não protegeria nenhuma chamada real — os dois trabalhos tinham que andar juntos.

Antes de reescrever os repositórios, uma auditoria (2 agentes de exploração + verificação manual) encontrou bloqueadores reais no Domain que impediam o EF Core de sequer materializar os objetos:

- `CNPJ` e `Monetario` guardavam o valor num campo `private` sem nenhum getter público — impossível de ler de fora, logo impossível de mapear.
- Várias entidades sem construtor nenhum ou sem construtor parameterless (`Contrato`, `PlanoContrato`, `Ficha`, `TelefoneAcademia`, `TelefoneUsuario`, `Administrador`) — o EF Core materializa via reflexão e precisa de um ctor vazio.
- `Agenda.IdAgenda` era `int`, sempre setado para `0` (nunca funcionou de verdade); resto do domínio usa `Guid`.
- `Avaliacao.cs` já tinha Data Annotations (`[Table]`/`[Key]`/`[Column]`/`[Required]`) dentro do **Domain**, violando a regra (já documentada) de que o Domain não conhece EF Core/banco de dados.

Essas correções foram feitas primeiro (Fase 1), documentadas no plano de execução desta sessão, antes de qualquer código de acesso a dados novo.

## Decisão

- **EF Core Code-First**, mapeando as 25 tabelas do schema antigo — das quais 22 têm entidade real no Domain (as outras 3, `Video` e junções N:N puras, não têm entidade e não foram inventadas só para bater o número).
- **Fluent API** (`IEntityTypeConfiguration<T>`, uma classe por entidade em `src/Infrastructure/Database/Configurations/`) em vez de Data Annotations — inclui a remoção das annotations indevidas de `Avaliacao.cs`.
- **`ValueConverter<TModel, TProvider>`** para cada Value Object (`Nome`, `Email`, `CPF`, `CREF`, `CNPJ`, `Monetario`) em `src/Infrastructure/Database/Converters/` — a entidade continua expondo o VO fortemente tipado; a conversão para o tipo primitivo do banco acontece só na fronteira do EF Core.
- **`VitalitasDbContext`** (`src/Infrastructure/Database/Context/`) com um `DbSet<T>` por entidade e `OnModelCreating` aplicando todas as Configurations via `ApplyConfigurationsFromAssembly`.
- **`VitalitasDbContextFactory`** (`IDesignTimeDbContextFactory<VitalitasDbContext>`) para o `dotnet ef` CLI funcionar sem subir a API inteira — lê `appsettings*.json` + User Secrets + variáveis de ambiente, igual ao host real.
- **`dotnet ef database update`** substitui rodar `CREATE.sql` manualmente. `Program.cs` chama `dbContext.Database.Migrate()` automaticamente em Development, na subida da aplicação.
- **`DevelopmentSeeder`** (`src/Infrastructure/Database/Seed/`) substitui `INSERT.sql`: roda condicionalmente (só se `Academias` estiver vazia) depois do `Migrate()`, criando o mínimo necessário para logar (1 Academia + 1 Usuário Gestor) — o resto dos dados de teste é criado através da própria API, não de um script de seed extenso.
- **Os 5 repositórios foram reescritos** para receber `VitalitasDbContext` via injeção de dependência em vez de `DbConnectionFactory`, mantendo exatamente as mesmas assinaturas de interface (`IUsuarioRepository`, `IAlunoRepository`, `IGestorRepository`, `IFichaMedicaRepository`, `IRefreshTokenRepository`) — nenhuma mudança na camada de Application/Use Cases foi necessária.
- **Nenhuma relação `HasOne`/`WithMany` foi configurada** — o Domain não tem propriedades de navegação entre entidades (ex.: `Aluno` não tem `Usuario Usuario { get; }`), só chaves estrangeiras escalares (`Guid`). Mapear FKs de banco sem navegação equivalente no C# não traria benefício de uso via EF Core; joins continuam feitos explicitamente em LINQ (`join ... on ... equals ...`) onde necessário.
- **`DbConnectionFactory`, os DTOs internos do Dapper (`UsuarioDB`, `RefreshTokenDB`) e os pacotes `Dapper`/`Microsoft.Data.SqlClient` foram removidos** — nada mais os referencia.

## Alternativas consideradas

- **Manter Dapper para leituras e EF Core só para escrita** — descartado. Multiplicaria a superfície de código (duas formas de acessar o mesmo dado) sem necessidade real de performance neste estágio, e o filtro de tenant (ADR 0013) precisa valer para leitura e escrita igualmente.
- **EF Core Database-First (scaffold a partir do banco existente)** — descartado porque o pedido explícito era Code-First a partir de agora, e o schema do `CREATE.sql` já tinha divergências conhecidas em relação às entidades do Domain (ex.: `Avaliacao`) que precisavam ser corrigidas no C#, não preservadas do banco antigo.
- **Mapear só as 7 tabelas que já tinham repositório funcionando**, adiando as outras 18 — descartado por decisão explícita do usuário ("mapear as 25 de uma vez"), para não deixar o `DbContext` incompleto logo na primeira versão.
- **Retornar tipos fortemente tipados (DTOs/records) em vez de `dynamic`/`ExpandoObject`** nos métodos de leitura ad-hoc (`ListarAluno`, `ListarUsuarios`, `ObterGestor`, `ListarUsuario`) — seria o ideal a longo prazo, mas exigiria alterar as assinaturas de `IGestorRepository`/`IAlunoRepository` e, em cascata, os Use Cases que já fazem `result.propriedade` assumindo `dynamic`. Fora do escopo desta migração (troca de ORM, não redesenho de contrato); mantido como dívida técnica conhecida.
  - Atenção específica: tipos anônimos (`new { ... }`) são `internal` por padrão — quando retornados como `dynamic` para o assembly `Application` (que é uma assembly diferente de `Infrastructure`), o binder dinâmico do C# só enxerga membros de `object`. Por isso, todo método que precisa devolver um objeto dinâmico consumido fora de `Infrastructure` usa `System.Dynamic.ExpandoObject`, não tipo anônimo — só `ExpandoObject` (e tipos que implementam `IDynamicMetaObjectProvider`) sustentam acesso dinâmico entre assemblies.
  - Atenção específica 2: EF Core não traduz acesso a membros de propriedades convertidas (`u.Email.Valor`) dentro de `Where`/`Select` para SQL — só a comparação da propriedade inteira (`u.Email == new Email(x)`) ou a materialização da entidade completa são traduzíveis. Onde era necessário projetar `.Valor` de um Value Object, a entidade completa é materializada primeiro (`.ToList()`/`.FirstOrDefault()` sobre a entidade), e o `.Valor` é lido depois, em memória — não dentro da expressão LINQ traduzida para SQL.

## Consequências

- Qualquer ambiente novo agora precisa só de `dotnet ef database update` (ou o `Migrate()` automático em Development) — não há mais passo manual de rodar `.sql` no SSMS.
- Alterações de schema passam a ter histórico versionado (`src/Infrastructure/Database/Migrations/`), revisável em code review como qualquer outro código.
- `CREATE.sql`/`INSERT.sql`/`SELECT.sql` ficaram órfãos — arquivados em `docs/archive/sql-scripts-legado/` (movidos de `src/Infrastructure/Database/Scripts/` em 2026-07-28) como referência histórica, não referenciados por nenhum código ou processo de deploy.
- Dois defeitos pré-existentes na camada de Application foram descobertos durante o smoke test desta migração (não introduzidos por ela, e não corrigidos aqui por estarem fora do escopo de "trocar o ORM"):
  1. `GestorRepository.ListarUsuario`/`GestorUC.ListarUsuario` usa um `switch` com números de caso (`1=Aluno, 2=Instrutor, 3=Funcionario, 4=Gestor`) que não correspondem ao enum real `TipoUsuario` (`Instrutor=1, Aluno=2, Gestor=3, Administrador=4`, sem valor para Funcionário) — qualquer usuário real quebra esse endpoint.
  2. `FichaMedicaUC.CriarFichaMedica` chama `new StatusHTTP(201, "mensagem")`, que casa com uma sobrecarga `(int, string)` que não popula `Message`/`Code`/`Sucess` — a ficha médica é persistida corretamente, mas a resposta HTTP sempre reporta falha.
