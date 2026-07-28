# Configurações do Backend — Vitalitas

> Página de referência para a Wiki do Azure DevOps. Cole este conteúdo numa nova página da wiki (ex: `Backend / Configurações`). Complementa — não substitui — os ADRs em `docs/adr/` do repositório, que explicam o *porquê* de cada decisão.

## Visão geral

O backend (.NET 9) resolve configuração através do pipeline padrão do `WebApplication.CreateBuilder`, sem nenhuma customização. Nenhum segredo real fica versionado no repositório — `appsettings.json` traz os campos sensíveis vazios de propósito, e o valor real é suprido localmente (User Secrets ou `appsettings.Development.json`) ou em ambiente hospedado (variável de ambiente / Azure Key Vault, quando existir).

## Ordem de precedência

Cada fonte abaixo sobrescreve a anterior. É o comportamento nativo do host ASP.NET Core, nenhum código foi escrito para isso (ver [ADR-0011](../adr/0011-environment-variables-provider.md)):

| Ordem | Fonte | Onde vive | Versionado? |
|---|---|---|---|
| 1 (mais baixa) | `appsettings.json` | `src/API/appsettings.json` | Sim |
| 2 | `appsettings.{ASPNETCORE_ENVIRONMENT}.json` | `src/API/appsettings.Development.json` (hoje só existe para Development) | **Não** |
| 3 | User Secrets | Fora do repositório, por máquina; ativo só quando `ASPNETCORE_ENVIRONMENT=Development` | Não (nem existe no disco do repo) |
| 4 | Variáveis de ambiente do processo | Definidas pelo SO, pelo Azure App Service, ou por container | Não |
| 5 (mais alta) | Argumentos de linha de comando | Passados no `dotnet run -- --chave=valor` | N/A |

**Convenção de nome para variável de ambiente**: troque `:` por `__` (dois underscores). Exemplo: a chave `Jwt:Key` vira a variável de ambiente `Jwt__Key`.

## Tabela completa de parâmetros

| Chave (`appsettings.json`) | Tipo | Obrigatório | Valor padrão versionado | Descrição | Onde é lido no código | Ambiente aplicável |
|---|---|---|---|---|---|---|
| `ConnectionStrings:ConexaoPadrao` | `string` | **Sim** | `""` (vazio) | Connection string do SQL Server, usada pelo `VitalitasDbContext` (Entity Framework Core). | `Infrastructure/Database/Context/VitalitasDbContext.cs`, `Infrastructure/Extensions/InfrastructureServiceExtensions.cs` | Todos — valor **diferente por ambiente**, aponta para o banco daquele ambiente |
| `Jwt:Key` | `string` (≥ 32 caracteres) | **Sim** | `""` (vazio) | Chave simétrica (HMAC-SHA256) usada para assinar e validar o access token JWT. | `API/Settings/JwtSettings.cs` → `JwtService.cs` (geração/validação), `Program.cs` (middleware `AddJwtBearer`) | Todos — valor **diferente por ambiente**, nunca reaproveitar entre Dev/Staging/Prod |
| `Jwt:Issuer` | `string` | **Sim** | `"VitalitasBackend"` | Claim `iss` (emissor) do token, validado na autenticação. | Mesmo que acima | Todos — normalmente o mesmo valor em todos os ambientes |
| `Jwt:Audience` | `string` | **Sim** | `"VitalitasBackendUsers"` | Claim `aud` (audiência) do token, validado na autenticação. | Mesmo que acima | Todos — normalmente o mesmo valor em todos os ambientes |
| `Jwt:DurationInMinutes` | `int` (> 0) | **Sim** | `15` | Tempo de vida do access token, em minutos. | `API/Settings/JwtSettings.cs` | Todos |
| `Jwt:RefreshTokenDurationInDays` | `int` (> 0) | Não (default `7` no código) | `7` | Tempo de vida do refresh token, em dias. | `Application/Token/Settings/RefreshTokenSettings.cs` | Todos |
| `Logging:LogLevel:Default` | `string` (enum: `Trace`, `Debug`, `Information`, `Warning`, `Error`, `Critical`, `None`) | Não | `"Information"` | Verbosidade padrão de log de toda a aplicação. | Gerido internamente pelo `Microsoft.Extensions.Logging` — não há classe própria de config | Todos — recomendado `Information` em Dev, `Warning` ou mais restrito em Staging/Prod (menos ruído, menor custo de log) |
| `Logging:LogLevel:Microsoft.AspNetCore` | `string` (mesmo enum acima) | Não | `"Warning"` | Verbosidade de log específica dos componentes internos do ASP.NET Core. | Idem acima | Todos |
| `AllowedHosts` | `string` (`*` ou lista de hosts separada por `;`) | Não | `"*"` | Filtro de host header aceito pela aplicação (middleware de host filtering do ASP.NET Core). | Gerido internamente pelo framework | Todos — `*` aceitável em Dev; **recomendado restringir ao(s) domínio(s) real(is)** em Staging/Produção, por segurança |

## Segredos vs. configuração não-sensível

| Categoria | Exemplos | Onde deve morar |
|---|---|---|
| Segredo (nunca versionar) | `Jwt:Key`, `ConnectionStrings:ConexaoPadrao` (contém credencial) | User Secrets (dev), variável de ambiente (Staging/Prod), Azure Key Vault (quando provisionado) |
| Configuração não-sensível | `Jwt:Issuer`, `Jwt:Audience`, `Jwt:DurationInMinutes`, `Logging:*`, `AllowedHosts` | Pode ficar versionado em `appsettings.json` |

> **Nota sobre Azure Key Vault**: ainda **não está provisionado** para o Vitalitas — nenhum vault, nenhuma política de acesso, nenhum recurso Azure existe hoje para este projeto (verificado via `az account show` sem sessão ativa, ausência de qualquer Bicep/ARM/Terraform no repositório, e nenhuma referência a Key Vault no código). Quando for provisionado, a integração se encaixa naturalmente no passo 4 da ordem de precedência acima — o provider de Key Vault do .NET se comporta como mais uma fonte de configuração, sem precisar mudar `JwtSettings`/`RefreshTokenSettings` nem nenhum código consumidor.

## Configurações que ainda não existem (fora do escopo desta página)

- **`Tenancy`** — PBI separado, estrutura multi-tenant ainda não implementada.
- **`Notificações`** — nenhum código ou seção de config relacionada existe hoje.
- **CORS parametrizado** — a origem permitida (`http://localhost:3000`) está fixa em `Program.cs`, não é lida de `appsettings.json`. Vira candidato a parâmetro (`Cors:AllowedOrigins`) quando houver um front-end de Staging/Produção com origem diferente do localhost.
- **`appsettings.Staging.json` / `appsettings.Production.json`** — não existem; não há ainda infraestrutura de deploy para Staging/Production (ver ADR relacionado). Testado e confirmado que a aplicação já funciona com um único build variando só `ASPNETCORE_ENVIRONMENT` + variáveis de ambiente, sem precisar desses arquivos.

## Referências

- [ADR-0008 — Gestão de segredos em `appsettings`](../adr/0008-gestao-de-segredos-appsettings.md)
- [ADR-0010 — Options Pattern para a configuração `Jwt`](../adr/0010-options-pattern-para-configuracao-jwt.md)
- [ADR-0011 — Environment Variables Provider](../adr/0011-environment-variables-provider.md)
- [ADR-0012 — Validação: build único roda em múltiplos ambientes](../adr/0012-validacao-build-unico-multi-ambiente.md)
- [`docs/Arquitetura_backend.md`](../Arquitetura_backend.md) — estrutura geral do backend
