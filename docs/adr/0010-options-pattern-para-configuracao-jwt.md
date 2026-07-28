# 0010 — Options Pattern para a configuração `Jwt`

## Status

Implementado.

## Contexto

A seção `Jwt` do `appsettings.json` (`Key`, `Issuer`, `Audience`, `DurationInMinutes`, `RefreshTokenDurationInDays`) era lida via `IConfiguration` indexado por string em quatro pontos diferentes do código:

- `JwtService.cs` (API) — `configuration["Jwt:Key"]`, `["Jwt:Issuer"]`, `["Jwt:Audience"]`, `["Jwt:DurationInMinutes"]`.
- `Program.cs` — as mesmas três primeiras chaves, lidas de novo (duplicado do que `JwtService` já lia) para configurar `TokenValidationParameters` do middleware `AddJwtBearer`.
- `Program.cs`, função `ValidateJwtConfiguration` — ~30 linhas de `if (string.IsNullOrWhiteSpace(...))` escritas à mão, validando as mesmas quatro chaves antes do app subir.
- `ApplicationServiceExtensions.cs` — `configuration["Jwt:RefreshTokenDurationInDays"]`, convertido manualmente com `int.Parse(... ?? "7")` para construir `RefreshTokenSettings` (uma classe de settings já existente, mas montada na mão, sem nenhum mecanismo de binding).

Nenhum ponto usava `IOptions<T>`/`IOptionsMonitor<T>`/`IOptionsSnapshot<T>`. `Jwt:Key` especificamente era lida em dois lugares (`JwtService` e `Program.cs`) sem garantia de que os dois liam exatamente a mesma coisa da mesma forma.

## Decisão

Criadas duas classes de configuração tipada, uma por camada que efetivamente consome a seção `Jwt`:

- **`API/Settings/JwtSettings.cs`** — `Key`, `Issuer`, `Audience`, `DurationInMinutes`, com `[Required]`/`[Range]` do `System.ComponentModel.DataAnnotations`. Consumida por `JwtService` via `IOptions<JwtSettings>`.
- **`Application/Token/Settings/RefreshTokenSettings.cs`** — já existia, mas foi convertida de classe com construtor único (`RefreshTokenSettings(int durationInDays)`, propriedade só leitura) para POCO vinculável (`RefreshTokenDurationInDays { get; set; }`, nome batendo exatamente com a chave do JSON para binding automático). Consumida por `RefreshTokenUC` e `UsuarioUC` via `IOptions<RefreshTokenSettings>`.

`RefreshTokenSettings` continua em `Application`, não em `API` — apesar de os dois lerem a mesma seção `Jwt` do JSON, são consumidos por camadas diferentes: `JwtSettings` só é usada dentro da API (geração/validação de token, middleware `AddJwtBearer`); `RefreshTokenDurationInDays` é uma regra de negócio consumida por Use Cases da Application (`RefreshTokenUC`, `UsuarioUC`). Como `Application` não referencia `API` (ver ADR 0005), não seria possível usar uma única classe compartilhada sem violar a direção de dependência.

Em `Program.cs`, a validação manual de 30 linhas foi substituída por:

```csharp
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("Seção 'Jwt' ausente no appsettings.json.");
Validator.ValidateObject(jwtSettings, new ValidationContext(jwtSettings), validateAllProperties: true);

builder.Services.AddSingleton(Options.Create(jwtSettings));
```

O mesmo `jwtSettings` já validado é reutilizado na configuração do `AddJwtBearer` (`TokenValidationParameters`), eliminando a segunda leitura duplicada de `Jwt:Key`/`Issuer`/`Audience` que existia antes.

Em `ApplicationServiceExtensions.cs`, o registro manual virou:

```csharp
services.Configure<RefreshTokenSettings>(configuration.GetSection("Jwt"));
```

## Alternativas consideradas

- **`IOptionsMonitor<T>` (reload em tempo real)** — descartada. Não há requisito de alterar `Jwt:Key`/`Issuer`/`Audience` com o processo rodando; trocar a chave de assinatura em runtime invalidaria tokens já emitidos de forma imprevisível. `IOptions<T>` (fixo, lido uma vez no startup) é o comportamento correto aqui.
- **`services.AddOptions<T>().ValidateDataAnnotations().ValidateOnStart()`** (validação declarativa via pipeline do próprio Options) — avaliada e descartada em favor de `Validator.ValidateObject` direto em `Program.cs`. O pipeline declarativo exigiria o pacote `Microsoft.Extensions.Options.DataAnnotations` a mais, e a validação só dispara no primeiro `IOptions<T>.Value` resolvido (que pode ser tarde, dependendo da ordem de resolução de dependências) — enquanto `Validator.ValidateObject` chamado direto falha imediatamente, na primeira linha do `Program.cs`, antes de qualquer outra coisa ser registrada no container.
- **Unificar `JwtSettings` e `RefreshTokenSettings` numa única classe** — descartada pela restrição de dependência entre `Application` e `API` explicada acima.

## Consequências

- `Jwt:Key` agora é lida do `IConfiguration` **uma única vez**, em `Program.cs` — todo o resto do código (`JwtService`, `RefreshTokenUC`, `UsuarioUC`) recebe o valor já validado via injeção de dependência, nunca lendo `IConfiguration` diretamente.
- Falha de configuração (`Jwt:Key` ausente, `DurationInMinutes` negativo, etc.) agora impede o processo de subir, com mensagem de erro apontando exatamente qual campo falhou — mesmo comportamento de antes, só que a validação vive numa classe (`JwtSettings`) em vez de uma função solta com `if`s repetidos.
- Novos pacotes: `Microsoft.Extensions.Options` e `Microsoft.Extensions.Options.ConfigurationExtensions`, adicionados ao `Directory.Packages.props` (ADR 0009) e referenciados só em `Application.csproj` — `API.csproj` não precisou de pacote novo porque o SDK `Microsoft.NET.Sdk.Web` já traz isso via o shared framework do ASP.NET Core.
- Testado localmente nos dois caminhos: com `dotnet user-secrets` configurado (app sobe normalmente) e sem (`Validator.ValidateObject` lança `ValidationException` imediatamente, app não sobe).
- Escopo desta ADR é só a seção `Jwt`. `ConnectionStrings`, `Logging` e `AllowedHosts` foram avaliados e descartados como candidatos (ver conversa que originou esta decisão) — `Logging`/`AllowedHosts` já são geridos pelo próprio framework, `ConnectionStrings` já tem API dedicada (`IConfiguration.GetConnectionString`). Uma futura seção `Tenancy` (item de outro PBI) seria candidata natural ao mesmo padrão quando for criada.
