# 0011 — Adoção de `dotnet user-secrets` para segredos locais (supersede ADR-0008)

## Status

Implementado. **Supersede o [ADR-0008](0008-gestao-de-segredos-appsettings.md)**.

## Contexto

O ADR-0008 documentou a criação de `src/API/appsettings.Development.json.example` como forma de um novo desenvolvedor saber quais chaves preencher localmente (`ConnectionStrings:ConexaoPadrao` e `Jwt:Key`), copiando o arquivo `.example` para `appsettings.Development.json` (ignorado pelo `.gitignore`) e preenchendo os valores à mão.

Naquele momento, o ADR-0008 já considerou `dotnet user-secrets` como alternativa e a descartou "por ora", para não introduzir uma ferramenta nova no meio da revisão de arquitetura, deixando registrado que poderia virar um ADR futuro "se o time achar valioso".

O cenário mudou: o banco deixou de ser uma instância local por desenvolvedor e passou a ser uma instância única do Azure SQL centralizada (ver [ADR-0010](0010-adocao-ef-core-azure-sql.md)). Isso muda o cálculo de risco do segredo em `ConnectionStrings:ConexaoPadrao` — antes apontava para um SQL Server local/Express de cada um; agora aponta para um recurso real e compartilhado no Azure, com um usuário/senha (ou connection string com token) que dá acesso ao mesmo banco que todo o time usa. Um arquivo `.example` só com placeholder não muda esse risco, mas reforça o hábito de copiar/colar segredos em arquivos dentro da árvore do projeto — um vetor de vazamento que já se concretizou uma vez neste repositório (ver ADR-0008, contexto do commit com credencial commitada em `appsettings.json`).

## Decisão

Adotado `dotnet user-secrets` como forma padrão de configurar segredos locais no projeto `src/API` (que já tinha `UserSecretsId` definido no `.csproj` desde a revisão anterior, mas sem uso ativo):

- Segredos (`ConnectionStrings:ConexaoPadrao`, `Jwt:Key`) passam a ser configurados via `dotnet user-secrets set`, armazenados pelo SDK do .NET fora da árvore do repositório (`%APPDATA%\Microsoft\UserSecrets\<UserSecretsId>\secrets.json` no Windows) — fisicamente impossível de commitar por acidente, ao contrário de um arquivo dentro do projeto.
- `src/API/appsettings.Development.json.example` **foi mantido** como documentação viva de quais chaves existem e seu formato esperado — só deixou de ser o método recomendado de preenchê-las. Ver README, seção "Configuração Inicial (Devs)".
- Foi criada `Infrastructure/Database/Context/AppDbContextFactory.cs` (`IDesignTimeDbContextFactory<AppDbContext>`), que lê a mesma connection string via user-secrets para permitir que `dotnet ef migrations add`/`database update` funcionem sem depender do boot completo da API (que hoje valida a configuração de JWT na inicialização e lançaria exceção se os segredos não estivessem presentes).

## Alternativas consideradas

- **Manter apenas `appsettings.Development.json.example`** (não reverter o ADR-0008) — era a opção mais conservadora, mas mantém o segredo do Azure SQL compartilhado guardado em texto plano dentro da árvore do projeto (ainda que num arquivo ignorado pelo git), o que é mais fácil de vazar por engano (ex.: `git add -A`, zip do projeto inteiro, backup de pasta) do que um segredo que vive fora do repositório por padrão da própria ferramenta.
- **Azure Key Vault / variáveis de ambiente de CI apenas** — mais robusto para produção/pipeline, mas não resolve o fluxo de desenvolvimento local (que ainda precisa de *algum* jeito de configurar a connection string na máquina do dev); `user-secrets` e Key Vault não são mutuamente exclusivos — Key Vault para produção/pipeline fica como possível ADR futuro, fora do escopo desta mudança que é sobre o fluxo local.

## Consequências

- Onboarding de um novo dev passa a incluir os comandos `dotnet user-secrets set` (documentados no README) em vez de copiar/editar um arquivo `.json`.
- Reduz a chance de um segredo do Azure SQL compartilhado ser commitado por acidente, já que ele nunca chega a existir como arquivo dentro da pasta do projeto.
- `dotnet ef` (CLI) precisa dos mesmos segredos para funcionar localmente (migrations/model bater com o banco real) — resolvido via `AppDbContextFactory`, que lê o mesmo `UserSecretsId` da API.
- Pendência que este ADR não resolve: a connection string real do Azure SQL usada em CI/CD (Azure DevOps) precisa ser configurada como variável secreta do pipeline (Library > Variable Group), não em `user-secrets` (que é só para a máquina local do dev) — ver comentário no template de migration em `azure-pipelines.yml`.
