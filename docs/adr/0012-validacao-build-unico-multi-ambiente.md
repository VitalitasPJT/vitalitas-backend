# 0012 — Validação: build único roda em múltiplos ambientes sem rebuild

## Status

Implementado (verificado por teste manual — sem mudança de código).

## Contexto

Task do backlog pedia para validar que um único artefato de build (`dotnet build`) consegue rodar em Development/Staging/Production apenas variando configuração — sem recompilar — como pré-condição para qualquer pipeline de deploy futuro (build uma vez, promove o mesmo artefato entre ambientes).

Essa validação depende diretamente de duas decisões já tomadas e verificadas:

- ADR 0010 — configuração tipada via Options Pattern, sem `IConfiguration` espalhado.
- ADR 0011 — Environment Variables Provider ativo por padrão via `WebApplication.CreateBuilder`.

Faltava a prova empírica de que essas duas coisas juntas realmente permitem trocar de ambiente sem rebuild — não bastava argumento teórico.

## Decisão

Nenhuma mudança de código. Foi feito um teste manual reproduzível:

1. `dotnet build src/API/Vitalitas.API.csproj` — um único build, gerando `Vitalitas.API.dll`.
2. Executado esse mesmo `.dll` (via `dotnet Vitalitas.API.dll`, não `dotnet run`, que reavalia o build a cada chamada) duas vezes, sem nenhum novo build entre as execuções, variando só `ASPNETCORE_ENVIRONMENT` e a variável de ambiente `Jwt__Key`.

### Resultado

| Execução | `ASPNETCORE_ENVIRONMENT` | `Hosting environment` reportado no log | `GET /swagger/Aluno/swagger.json` |
|---|---|---|---|
| 1 | `Development` | `Development` | `200 OK` |
| 2 | `Production` | `Production` | `404 Not Found` |

O Swagger só fica acessível em `Development` por causa do `if (app.Environment.IsDevelopment()) { app.UseSwagger(); ... }` já existente em `Program.cs` — nenhum código foi escrito para este teste, só usado como sinal observável de que o `.dll` reconheceu corretamente o ambiente e mudou de comportamento em runtime.

## Alternativas consideradas

- **Criar `appsettings.Staging.json`/`appsettings.Production.json` antes de validar** — descartado como pré-requisito. O teste prova que a troca de ambiente funciona mesmo sem esses arquivos existirem, porque a config necessária (`Jwt:Key`) foi suprida via variável de ambiente (ADR 0011). Criar esses arquivos continua sendo uma task separada, bloqueada por falta de infraestrutura de deploy real (ver task correspondente).
- **Testar via `dotnet run`** — descartado; `dotnet run` reavalia/reconstrói o projeto a cada chamada, o que não provaria "sem rebuild". Rodar o `.dll` compilado diretamente foi a forma de garantir que era exatamente o mesmo binário nas duas execuções.

## Consequências

- Confirma que o artefato de build é agnóstico a ambiente — pré-condição satisfeita para qualquer pipeline futuro que faça "build once, deploy many" (build único, promovido entre Dev → Staging → Production sem recompilar).
- Reforça o valor prático dos ADRs 0010 e 0011: a combinação de configuração tipada + provider de variável de ambiente é o que torna essa troca possível sem tocar em código.
- Não foi testado neste ciclo: comportamento com `appsettings.{Environment}.json` presente (arquivo não existe ainda para Staging/Production) nem integração real com Azure App Service — a validação cobre o mecanismo do host, não a infraestrutura de nuvem em si.
