# 0006 — Remover código morto e comentado

## Status

Implementado.

## Contexto

Onze arquivos continham blocos de código comentado, sobras de iterações anteriores do backend. A maioria eram métodos individuais comentados dentro de classes que já tinham código funcionando (ex: um `CriarAluno` comentado dentro de `AlunoUC.cs`, que já tem `AtualizarObjetivo`, `ListarAluno`, etc. funcionando). Três arquivos eram um caso diferente e mais sério: `AgendaDBController.cs`, `AvaliacaoDBController.cs` e `FichasDBController.cs` (treino) eram **inteiramente** código comentado de uma versão anterior baseada em Entity Framework (`Contexto`, `DbSet`, LINQ-to-Entities) — sobrava só uma classe vazia com `[ApiController]`/`[Route]`, registrando uma rota que não fazia nada. `AvaliacaoDBController.cs` também tinha dois campos não usados (`_calculosFeminino`, `_calculosMasculino`) gerando warnings de build (`CS0169`, `CS8618`).

## Decisão

Para os 8 arquivos com métodos pontuais comentados dentro de classes funcionando, os blocos foram removidos sem discussão — eram claramente lixo de iteração, sem ambiguidade sobre o que fazer.

Para os 3 controllers inteiramente mortos, a decisão foi levada explicitamente ao usuário antes de agir, com três opções: apagar, manter como stub vazio, ou não mexer. **Decisão: apagar os 3 arquivos.** Referenciavam um ORM (Entity Framework) que o projeto não usa mais — toda a Infrastructure atual é Dapper — e não registravam nenhum endpoint funcional. Mantê-los como stub vazio (`[ApiController]`/`[Route]` sem métodos) foi descartado porque uma rota registrada sem handler é ruído, não sinalização útil.

## Alternativas consideradas

- **Manter os 3 controllers como stub vazio**, sinalizando "essa rota vai existir" — rejeitada; um controller vazio não comunica nada que um comentário no backlog não comunique melhor, e mantém código morto compilando.
- **Não mexer nos 3 controllers neste bloco**, só limpar os outros 8 — rejeitada; a limpeza de código morto perderia o próprio propósito se deixasse o pior caso de fora.

## Consequências

- Se Agenda, Avaliação (perimetria/dobras cutâneas) ou Treino virarem features reais no futuro, a implementação começa do zero seguindo o padrão Dapper + feature-folder do resto do projeto (Domain → Application → Infrastructure → API), não reaproveitando as sobras de EF Core.
- Os warnings de build `CS0169`/`CS8618` sobre `_calculosFeminino`/`_calculosMasculino` desapareceram junto com o arquivo que os continha.
- `docs/Arquitetura_backend.md` documenta explicitamente que esses 3 controllers existiram e por que foram removidos, para não parecer que a feature nunca foi cogitada.
