# 0009 — Gerenciamento centralizado de versões (Central Package Management)

## Status

Implementado.

## Contexto

Cada um dos quatro `.csproj` (`API`, `Application`, `Infrastructure`, `Domain`) declarava a versão de cada pacote NuGet individualmente, via `<PackageReference Include="X" Version="Y" />`. Não existia `Directory.Packages.props` nem `Directory.Build.props` no repositório.

Isso não é um problema hipotético: três pacotes já eram referenciados em mais de um projeto ao mesmo tempo —

| Pacote | Onde aparecia |
|---|---|
| `Dapper` | `API`, `Infrastructure` |
| `Microsoft.Extensions.Configuration.Abstractions` | `Application`, `Infrastructure` |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | `Application`, `Infrastructure` |

Nos três casos as versões coincidiam (`2.1.72` e `10.0.5`), mas por coincidência — nada impedia alguém de atualizar o `Dapper` só no `API.csproj` num PR futuro e deixar o `Infrastructure.csproj` para trás, silenciosamente. Também foi identificado um outlier de versão: `Microsoft.AspNetCore.Authentication.JwtBearer` em `8.0.0`, enquanto o projeto inteiro roda em `net9.0` e os demais pacotes `Microsoft.Extensions.*` já estavam em `10.0.5` — não chega a ser uma divergência entre projetos (só é referenciado na API), mas é o tipo de inconsistência que fica mais visível com tudo num lugar só.

## Decisão

Criado `Directory.Packages.props` na raiz do repositório com `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>` e um `<PackageVersion Include="X" Version="Y" />` para cada um dos 7 pacotes únicos usados em todo o projeto. Os `<PackageReference>` nos 4 `.csproj` perderam o atributo `Version`, ficando só `<PackageReference Include="X" />` — a versão real vem exclusivamente do `Directory.Packages.props`.

Nenhuma versão foi alterada nesta mudança — é reorganização pura, sem impacto de runtime. Validado com `dotnet build` (0 erros) depois da migração.

## Alternativas consideradas

- **Não fazer nada, confiar em revisão manual de PR para pegar divergência de versão** — era o estado anterior; descartado porque divergência de versão entre projetos do mesmo repositório é exatamente o tipo de erro silencioso que passa despercebido em review (a menos que o revisor compare os 4 arquivos lado a lado toda vez).
- **`Directory.Build.props` com propriedades MSBuild compartilhadas em vez de `Directory.Packages.props`** — resolve outro problema (propriedades comuns como `TargetFramework`, `Nullable`), não o de versionamento de pacote; não é mutuamente exclusivo com CPM, mas é um ADR separado se o time quiser essa consolidação depois (hoje `TargetFramework`/`Nullable`/`ImplicitUsings` ainda são repetidos em cada `.csproj`).

## Consequências

- Atualizar um pacote usado por mais de um projeto (ex: Dapper) agora é uma mudança em um único lugar (`Directory.Packages.props`), não uma busca manual por todos os `.csproj` que o referenciam.
- Adicionar um pacote novo a um projeto passa a ser dois passos: `<PackageVersion>` no `Directory.Packages.props` (se ainda não existir) + `<PackageReference Include="X" />` sem versão no `.csproj` do projeto. Se alguém esquecer o primeiro passo, o build falha imediatamente com erro claro do MSBuild, em vez de compilar silenciosamente com uma versão errada.
- O outlier `Microsoft.AspNetCore.Authentication.JwtBearer 8.0.0` foi mantido como está nesta mudança — centralizar não é o mesmo que atualizar; decidir se vale a pena subir para uma versão alinhada ao `net9.0` fica como item separado, fora do escopo deste ADR.
