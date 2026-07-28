# 0011 — Environment Variables Provider para override de configuração

## Status

Implementado (nenhuma mudança de código necessária — verificado e documentado).

## Contexto

Uma task do backlog pedia para "Configurar Environment Variables Provider", permitindo sobrescrever valores do `appsettings.json` via variáveis de ambiente do SO ou do Azure App Service, sem alterar código — necessário para configurar segredos/valores por ambiente de deploy sem versionar nada sensível e sem precisar de um `appsettings.{Environment}.json` por alvo de infraestrutura.

Antes de assumir que isso precisava ser implementado, foi verificado como `Program.cs` constrói a configuração: `var builder = WebApplication.CreateBuilder(args);`, sem nenhuma customização de `builder.Configuration.Sources` (nem `.Clear()`, nem remoção de provedores).

## Decisão

Nenhuma mudança de código foi feita — `WebApplication.CreateBuilder(args)` já monta, por padrão, o seguinte pipeline de configuração (cada fonte sobrepõe a anterior):

1. `appsettings.json`
2. `appsettings.{ASPNETCORE_ENVIRONMENT}.json`
3. User Secrets (só quando `ASPNETCORE_ENVIRONMENT=Development`)
4. **Variáveis de ambiente do processo**
5. Argumentos de linha de comando

Isso já habilita, sem nenhum código adicional, sobrescrever qualquer chave via variável de ambiente usando a notação `Chave__Subchave` (dois underscores substituem o `:` do `IConfiguration`) — ex: `Jwt__Key`, `ConnectionStrings__ConexaoPadrao`. É exatamente o mecanismo que o Azure App Service usa: valores configurados no painel "Configuration" do App Service são injetados como variáveis de ambiente do processo, reconhecidas automaticamente por esse pipeline padrão.

### Verificação feita

Testado localmente, com o app real (não é só leitura de documentação do framework):

```bash
# Sem override nenhum (Jwt:Key vazio no appsettings.json) → falha, como esperado
dotnet run
# ValidationException: 'Jwt:Key' está ausente ou vazio.

# Com override via variável de ambiente, sem tocar em nenhum código → sobe normalmente
Jwt__Key="Chave-De-Teste-Via-Environment-Variable-32chars" dotnet run
# Application started. Press Ctrl+C to shut down.
```

Isso comprova que a variável de ambiente é lida e tem prioridade suficiente para suprir um valor que o `appsettings.json` deixa vazio de propósito (ver ADR 0008).

## Alternativas consideradas

- **Chamar `builder.Configuration.AddEnvironmentVariables()` explicitamente** — desnecessário e redundante; `CreateBuilder` já adiciona esse provedor internamente. Adicionar de novo não muda o comportamento, só polui `Program.cs` com uma linha que sugere (incorretamente) que isso não estava coberto por padrão.
- **Implementar um provedor de configuração customizado para Azure App Service** — descartado; o App Service já expõe configuração como variáveis de ambiente do processo nativamente, não precisa de um provedor específico do Azure (isso só seria necessário para Azure App Configuration ou Key Vault, que são serviços diferentes, fora do escopo desta task).

## Consequências

- Nenhum código foi alterado — esta ADR documenta uma verificação, não uma implementação.
- Qualquer chave de configuração (`Jwt:Key`, `ConnectionStrings:ConexaoPadrao`, futuras seções como `Tenancy`) já pode ser sobrescrita em produção/staging via variável de ambiente, sem precisar criar `appsettings.Production.json`/`appsettings.Staging.json` (ver task relacionada, ainda bloqueada por falta de infraestrutura de deploy) nem versionar nenhum segredo.
- Ordem de precedência importa: se um dia for adicionado `appsettings.{Environment}.json`, ele ainda fica **abaixo** das variáveis de ambiente na prioridade — uma variável de ambiente sempre vence um valor de arquivo JSON, em qualquer ambiente.
- Fica registrado que essa "task de configuração" já estava satisfeita antes mesmo de existir Staging/Production reais — é comportamento do host, não algo que se "implementa" por cima.
