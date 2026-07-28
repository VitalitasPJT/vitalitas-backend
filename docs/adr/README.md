# Architecture Decision Records — Vitalitas Backend

Registro das decisões tomadas durante a revisão de arquitetura de 2026. Cada arquivo documenta o contexto, a decisão e as consequências de uma mudança estrutural específica.

| ADR | Título | Status |
|---|---|---|
| [0001](0001-alinhar-namespaces-a-estrutura-de-pastas.md) | Alinhar namespaces à estrutura de pastas | Implementado |
| [0002](0002-reorganizar-infrastructure-por-feature.md) | Reorganizar a Infrastructure por feature | Implementado |
| [0003](0003-padronizar-nomenclatura-de-pastas.md) | Padronizar nomenclatura de pastas (idioma, casing, singular/plural) | Implementado |
| [0004](0004-corrigir-nomes-de-arquivo-tipo-enganosos.md) | Corrigir nomes de arquivo/tipo enganosos | Implementado |
| [0005](0005-reorganizar-injecao-de-dependencia.md) | Reorganizar a injeção de dependência em `Program.cs` | Implementado |
| [0006](0006-remover-codigo-morto.md) | Remover código morto e comentado | Implementado |
| [0007](0007-eliminar-duplicacao-constructor.md) | Eliminar a duplicação da classe `Constructor` | Implementado |
| [0008](0008-gestao-de-segredos-appsettings.md) | Gestão de segredos em `appsettings` | Implementado |
| [0009](0009-gerenciamento-centralizado-de-versoes.md) | Gerenciamento centralizado de versões (Central Package Management) | Implementado |
| [0010](0010-options-pattern-para-configuracao-jwt.md) | Options Pattern para a configuração `Jwt` | Implementado |
| [0011](0011-environment-variables-provider.md) | Environment Variables Provider para override de configuração | Implementado (verificado, sem mudança de código) |
| [0012](0012-validacao-build-unico-multi-ambiente.md) | Validação: build único roda em múltiplos ambientes sem rebuild | Implementado (verificado por teste manual) |
| [0013](0013-multi-tenancy-shared-database-shared-schema.md) | Multi-Tenancy: Shared Database, Shared Schema | Backlog removido do board (2026-07-28); Fases 0–2 implementadas ficam como capacidade dormente |
| [0014](0014-migracao-dapper-para-ef-core.md) | Migração de Dapper (SQL manual) para Entity Framework Core | Implementado |
| [0015](0015-estrategia-tres-ambientes.md) | Estratégia de três ambientes: Development, Staging, Production | Implementado parcialmente (config local pronta; infra real de deploy pendente) |

## Formato

Cada ADR segue a estrutura:

- **Contexto** — o que motivou a decisão, o que foi encontrado no código.
- **Decisão** — o que foi feito.
- **Alternativas consideradas** — outras opções avaliadas e por que não foram escolhidas.
- **Consequências** — trade-offs, o que fica mais fácil, o que fica mais trabalhoso, pendências que a decisão deixa em aberto.
