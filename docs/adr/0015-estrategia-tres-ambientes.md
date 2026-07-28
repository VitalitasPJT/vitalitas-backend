# 0015 — Estratégia de três ambientes: Development, Staging, Production

## Status

Implementado parcialmente (estrutura de configuração local pronta; publicação/infra real de Staging e Production dependem de decisão futura sobre onde hospedar — fora do escopo desta sessão).

## Contexto

O PBI "Configurar Variáveis de Ambiente" pede três ambientes existentes e identificáveis (Development, Homologação/Staging, Produção), cada um com configuração e credenciais independentes, e uma estrutura pronta para publicação. Antes desta sessão só existia estrutura de configuração para Development (`appsettings.json` base + `appsettings.Development.json.example`) — Staging e Production não tinham nenhum arquivo, nem placeholder.

O usuário já decidiu explicitamente adiar qualquer provisionamento real de nuvem ("vou fazer o deploy no Azure depois") — então o que cabia fazer agora era preparar tudo que não depende de infraestrutura paga existir: a estrutura de configuração local, a validação de que o mecanismo de troca de ambiente funciona para os três valores (não só Dev/Prod, que é o que o ADR 0012 já tinha validado), e a documentação da estratégia.

## Decisão

- **Três arquivos de configuração versionados, sem segredos reais** (mesmo padrão do ADR 0008): `appsettings.json` (base, já existia), `appsettings.Staging.json` e `appsettings.Production.json` (novos, criados nesta sessão) — todos com `ConnectionStrings:ConexaoPadrao` e `Jwt:Key` vazios. Os valores reais de cada ambiente são supridos só em runtime, via variável de ambiente do processo (Staging/Production) ou User Secrets (Development local) — nunca commitados.
- **Identificação do ambiente**: exclusivamente via `ASPNETCORE_ENVIRONMENT` (mecanismo padrão do `WebApplication.CreateBuilder`), sem lógica própria. Validado nesta sessão rodando o mesmo `.dll` compilado (Release) com `ASPNETCORE_ENVIRONMENT=Staging` e as credenciais via variável de ambiente: o Swagger continuou desabilitado (comportamento gated por `IsDevelopment()`) e o endpoint `/usuario/login` respondeu corretamente contra o banco real — confirma que a troca de ambiente funciona de ponta a ponta para um terceiro valor além de Development/Production (que já tinham sido validados no ADR 0012).
- **`Logging:LogLevel:Default` mais restrito em Staging/Production** (`Warning`, contra `Information` em Development) — segue a recomendação já registrada na tabela de referência de configuração do README.
- **Regra explícita: dado de Produção nunca é usado em Development.** Não há enforcement automático disso no código (não existe "dado de produção" real ainda, já que não há ambiente de Produção provisionado) — é uma regra de processo, registrada aqui para não depender de memória de quem for provisionar o ambiente real depois.

## Alternativas consideradas

- **Não criar os arquivos de Staging/Production agora, esperar até ter infraestrutura real** — descartado. A estrutura de configuração (o "formato" de cada ambiente) é código, independe de onde a aplicação vai rodar, e criar os arquivos agora não implica nenhum custo nem decisão de infraestrutura — só documenta a forma, com valores vazios até existir um ambiente real para preencher.
- **Usar um único `appsettings.json` com seções por ambiente dentro do mesmo arquivo** — descartado; foge do padrão já estabelecido pelo `WebApplication.CreateBuilder` (`appsettings.{Environment}.json` como arquivo separado), que é o que já funciona e está documentado nos ADRs 0010/0011.

## Consequências

- A aplicação já está pronta, do ponto de vista de código, para rodar em três ambientes diferentes — falta só o provisionamento real (onde cada um roda, quem tem acesso, de onde vêm os valores reais de `ConnectionStrings`/`Jwt:Key`).
- HU-03 do PBI (publicar em cada ambiente) continua inteiramente bloqueada — é sobre execução real de deploy, que depende da decisão de hospedagem (Azure App Service, container, outro) ainda não tomada.
- HU-02 #2 e #4 (credenciais isoladas por ambiente, configurações protegidas por permissão) continuam parcialmente bloqueadas — Development tem isolamento real via User Secrets; Staging/Production não têm cofre de segredo nenhum até existir infraestrutura de nuvem provisionada (Key Vault ou equivalente).
- HU-04 #2 e #3 (acesso respeitando permissões, usuários só nos ambientes autorizados) dependem de IAM real (Azure RBAC, GitHub Environments com required reviewers, etc.) — não é algo que existe só com código, fica para quando a infraestrutura for provisionada.
