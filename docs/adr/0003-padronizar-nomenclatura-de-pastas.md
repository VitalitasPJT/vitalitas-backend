# 0003 — Padronizar nomenclatura de pastas (idioma, casing, singular/plural)

## Status

Implementado.

## Contexto

Depois do ADR 0001 alinhar namespace a pasta, ficou visível que a própria estrutura de pastas tinha inconsistências de nomenclatura que o ADR 0001 apenas preservou/refletiu, sem corrigir:

- **Idioma**: `Domain/features/users` (inglês) coexistindo com `Application/usuarios` (português) para a mesma feature — e dentro do próprio Domain, `academia`/`fichas`/`planos` (português) ao lado de `shared`/`token`/`users` (inglês).
- **Casing**: a maior parte das pastas em minúsculo (`usuarios`, `aluno`) convivendo com `Application/Token` em PascalCase.
- **Singular/plural**: `Application/fichas/FichaMedica/Request` e `/Response` no singular, enquanto `aluno`, `common` e `gestor` usavam `Requests`/`Responses` no plural.
- **Erro de digitação estrutural**: `Domain/features/users/funcionario/interface` no singular (único caso, todos os outros são `interfaces`).

## Decisão

Quatro decisões foram tomadas explicitamente com o usuário antes de qualquer alteração (via pergunta direta, dado o tamanho do impacto):

1. **Idioma**: português para nomes de domínio/negócio (`Usuarios`, `Academia`, `Planos`, `Fichas`, `Gestor`, `Aluno`...); inglês mantido para termos técnicos genéricos sem tradução natural no jargão do time (`Shared`, `Token`, `DTO`, `UseCase`, `Constructor`). Resultado prático: `Domain/features/users` → `Domain/Features/Usuarios`.
2. **Casing**: PascalCase em todas as pastas, sem exceção — combina com a convenção de namespace do C# e com o padrão .NET de pasta espelhando namespace.
3. **Singular/plural**: singular em tudo (`Request`, `Response`) — decisão de manter o padrão minoritário (`FichaMedica`) em vez do majoritário, por ser gramaticalmente mais correto em inglês técnico (uma pasta é uma categoria de coisa, não uma coleção).
4. **Sincronização**: cada rename de pasta veio acompanhado do ajuste de namespace/using correspondente na mesma passada, em vez de deixar pasta e namespace temporariamente dessincronizados — para não reabrir o mesmo problema que motivou o ADR 0001.

A execução usou `git mv` com uma técnica de rename em dois passos (`pasta` → `pasta__tmp` → `Pasta`) para os casos onde a mudança era só de capitalização — necessário porque o Windows/NTFS é case-insensitive para nomes de arquivo, e um `git mv usuarios Usuarios` direto não é reconhecido como rename nesse cenário.

## Alternativas consideradas

- **Inglês para tudo**: rejeitado — exigiria traduzir a maioria das pastas de domínio já em português (`usuarios`, `academia`, `planos`, `fichas`, `gestor`...), impacto muito maior que o inverso.
- **lowercase em tudo**: rejeitado em favor de PascalCase, para casar com a convenção já usada nos namespaces C#.
- **Plural em tudo (`Requests`/`Responses`)**: era o padrão majoritário no código antes desta decisão, mas foi descartado a favor do singular.
- **Renomear pasta primeiro, ajustar namespace depois em outra passada**: rejeitado para não deixar a base num estado inconsistente por mais tempo do que necessário.

## Consequências

- 74 renomeações de arquivo (detectadas pelo `git status` como `R`) nesta única etapa.
- Todo o vocabulário de nomenclatura do projeto ficou explicitado em `docs/Arquitetura_backend.md`, seção "Convenções de nomenclatura" — features novas devem seguir essas quatro regras sem precisar redescobrir o padrão por inspeção.
- Nenhuma colisão nome-de-tipo (ADR 0001) apareceu nesta etapa, porque nenhuma das mudanças introduziu um segmento de namespace igual a um nome de entidade que já não estivesse ali antes.
