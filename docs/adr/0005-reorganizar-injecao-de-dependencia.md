# 0005 — Reorganizar a injeção de dependência em `Program.cs`

## Status

Implementado.

## Contexto

Todo o cadastro de serviços (`builder.Services.AddScoped<...>()`) vivia solto e sequencial em `API/Program.cs` — cerca de 14 linhas registrando interfaces e implementações de Domain, Application e Infrastructure lado a lado, sem nenhuma separação por camada ou feature. `docs/Arquitetura_backend.md` já previa uma pasta `Extensions` na Infrastructure para esse propósito, mas ela nunca tinha sido criada.

## Decisão

Duas perguntas foram resolvidas explicitamente antes da implementação:

**Granularidade** — híbrido: `Program.cs` chama só `AddApplicationServices()` e `AddInfrastructureServices()`; cada um desses métodos públicos delega internamente para métodos privados por feature (`AddAlunoFeature()`, `AddGestorFeature()`, `AddTokenFeature()`, `AddFichaMedicaFeature()`, `AddUsuarioFeature()`). Mantém `Program.cs` mínimo e reflete a mesma granularidade por feature usada em todo o resto do projeto.

**Localização** — cada camada registra a si mesma: `Application/Extensions/ApplicationServiceExtensions.cs` e `Infrastructure/Extensions/InfrastructureServiceExtensions.cs`, cada um dentro do próprio projeto.

### Problema encontrado durante a implementação: `JwtService` não cabe em nenhum dos dois

`JwtService` é uma classe do projeto **API** (`API/Services/JwtService.cs`) que implementa tanto `API.Services.IJwtService` (contrato próprio da API) quanto `Application.Token.Service.ITokenService` (contrato da Application). Como `Application.csproj` só referencia `Domain.csproj` — não referencia `API.csproj`, e a referência entre eles só existe no sentido contrário (API → Application) — um `AddApplicationServices()` fisicamente dentro do projeto Application **não pode** compilar uma referência a `API.Services.JwtService`.

**Resolução**: criado um terceiro método de extensão, `AddApiServices()`, em `API/Extensions/ApiServiceExtensions.cs`, só para os dois registros que envolvem `JwtService`. `Program.cs` ficou com três chamadas:

```csharp
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApiServices();
```

Também foi necessário adicionar os pacotes `Microsoft.Extensions.DependencyInjection.Abstractions` (Application e Infrastructure) e `Microsoft.Extensions.Configuration.Abstractions` (Application) — nenhum dos dois projetos tinha referência a `IServiceCollection`/`IConfiguration` antes, porque nunca tinham precisado.

## Alternativas consideradas

- **2 métodos por camada, sem quebra por feature** — mais simples, mas inconsistente com o resto do projeto ser 100% organizado por feature; descartada.
- **1 método por feature, direto no `Program.cs`, misturando Application+Infrastructure na mesma chamada** — mais granular, mas misturaria dois projetos hoje separados na mesma unidade de registro; descartada.
- **Tudo centralizado num único arquivo na API** (`API/Extensions/ServiceCollectionExtensions.cs`) — mais fácil de achar tudo num lugar só, mas quebra o princípio de cada camada se auto-registrar (a API passaria a conhecer detalhes de registro do Infrastructure/Application); descartada em favor de cada camada se registrar.

## Consequências

- `Program.cs` caiu de ~24 linhas de DI solta para 3 chamadas.
- Existem agora 3 pastas `Extensions` (Application, Infrastructure, API) em vez de 1 só na Infrastructure como o documento original prometia — documentado explicitamente no `docs/Arquitetura_backend.md`, seção Extensions, com o motivo do `AddApiServices()`.
- Adicionar uma feature nova envolve adicionar um método privado em `ApplicationServiceExtensions` e outro em `InfrastructureServiceExtensions` — dois pontos a lembrar, mas ambos localizados e nomeados de forma previsível.
