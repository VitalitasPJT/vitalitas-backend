# 0001 — Alinhar namespaces à estrutura de pastas

## Status

Implementado.

## Contexto

A revisão de arquitetura anterior (commits `19ed1a7`, `ede963b`, `b53c08f`) reorganizou fisicamente os arquivos do backend em pastas por feature (`Domain/features/users/aluno`, `Application/usuarios/aluno`, etc.), seguindo o padrão descrito em `docs/Arquitetura_backend.md`. Mas essa reorganização foi só de arquivos — os `namespace` declarados dentro de cada `.cs` continuaram os antigos, planos e compartilhados entre features: praticamente todo `Domain/features/**/entities` usava `namespace Domain.Entities`, todo `Domain/features/**/interfaces` usava `namespace Domain.Interfaces`, e o `Application` inteiro usava `Application.DTOs`, `Application.Interfaces` ou `Application.Services` independente da feature.

Isso significava que a "arquitetura por features" só existia no explorador de arquivos. `Program.cs` e todo o código que fazia `using` continuava referenciando os namespaces genéricos de antes — quem navegasse pelo IDE via "ir para definição" caía nos mesmos namespaces flat de sempre, sem nenhuma relação com a pasta onde o arquivo realmente estava.

## Decisão

Cada namespace passou a refletir exatamente o caminho físico do arquivo. Exemplo: `src/Domain/features/users/aluno/entities/Aluno.cs` foi de `namespace Domain.Entities` para `namespace Domain.Features.Users.Aluno.Entities` (depois ajustado para `Usuarios` no ADR 0003).

Essa mudança foi aplicada de forma mecânica em ~90 arquivos, camada por camada (Domain → Application → Infrastructure → API → `Program.cs`), com validação por `dotnet build` a cada passo até chegar a zero erros.

## Problema encontrado: colisão nome-de-tipo vs. segmento-de-namespace

Ao renomear os namespaces, surgiu um efeito colateral do C#: quando uma entidade tem o mesmo nome do segmento de namespace da própria feature (ex: a classe `Academia` dentro de `Domain.Features.Academia`), uma referência não qualificada ao tipo `Academia` em código dentro dessa árvore de namespaces é resolvida pelo compilador como o *namespace* `Academia`, não o tipo — gerando `CS0118: "X" é um namespace, mas é usado como um tipo`.

Isso afetou `Academia`, `FichaMedica`, `Licenca`, `Administrador`, `Funcionario`, `Aluno` e `Instrutor` em pontos específicos onde o tipo é referenciado a partir de um namespace irmão ou pai que compartilha o mesmo nome.

**Resolução**: nesses pontos específicos, o tipo passou a ser referenciado com o caminho totalmente qualificado (ex: `Domain.Features.Academia.Entities.Academia`) em vez de um `using` + nome curto. Tentar resolver com um `using` de alias (`using Academia = Domain.Features.Academia.Entities.Academia;`) **não funciona** — a resolução de nomes do C# prioriza membros de namespace pai sobre diretivas de alias nesse cenário, então a única solução confiável é a qualificação completa no ponto de uso.

## Alternativas consideradas

- **Manter os namespaces antigos e só reorganizar pastas** — foi descartado porque é exatamente o estado insatisfatório encontrado (pasta e namespace dessincronizados), motivo original desta revisão.
- **Usar aliases de using para resolver as colisões** — tentado e descartado; não funciona de forma confiável (ver acima).

## Consequências

- Namespace e localização física do arquivo agora são a mesma informação — mover um arquivo de pasta exige mover o namespace junto (documentado como convenção obrigatória em `docs/Arquitetura_backend.md`).
- Ao criar uma entidade nova, evitar nomeá-la igual ao segmento de namespace da própria feature (ex: não criar uma classe `Gestor` livre dentro de `Domain.Features.Usuarios.Gestor` sem qualificação) — o problema de colisão descrito acima vai se repetir.
- O tamanho do diff foi grande (86+ arquivos só nesta etapa) porque tocou toda a árvore de namespaces do projeto de uma vez — foi deliberado fazer de uma vez para não deixar a base num estado misto (parte alinhada, parte não) por mais tempo do que o necessário.
