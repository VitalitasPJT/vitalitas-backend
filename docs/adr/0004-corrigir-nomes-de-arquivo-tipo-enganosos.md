# 0004 — Corrigir nomes de arquivo/tipo enganosos

## Status

Implementado.

## Contexto

Dois problemas de nomenclatura no nível de tipo (não de pasta) foram encontrados na revisão inicial:

1. **Arquivo e tipo com nomes diferentes**: `IUsuarioRepository.cs` declarava `public interface IUsuario` — e o mesmo padrão apareceu em `IAlunoRepository.cs`, que declarava `public interface IAluno`. O nome do arquivo prometia um contrato de repositório; o tipo real era um nome abreviado, inconsistente com `IGestorRepository`, `IFichaMedicaRepository` e `IAcademiaRepository`, que já seguiam a convenção correta.
2. **Classe "guarda-chuva" com nome errado**: `Application/usuarios/gestor/Responses/GertorRP.cs` (typo — faltando um "s" de "Gestor") continha uma única classe `GertorRP` com 9 responses aninhadas de features diferentes (Usuario, Aluno, Instrutor, Funcionario, Gestor) — todas as saídas do `IGestorUseCase`, mas todas dentro de um único arquivo/classe wrapper.

## Decisão

- `IUsuario` → `IUsuarioRepository` e `IAluno` → `IAlunoRepository`, com todas as referências (implementações em Infrastructure, injeção em `Program.cs`, campos privados nos Use Cases) atualizadas.
- `GertorRP.cs` foi removido. As 9 responses aninhadas viraram 9 arquivos próprios em `Usuarios/Gestor/Response/` (`CriarUsuarioResponse.cs`, `CriarAlunoResponse.cs`, `CriarInstrutorResponse.cs`, `CriarFuncionarioResponse.cs`, `CriarGestorResponse.cs`, `ListarAlunosResponse.cs`, `ListarUsuariosResponse.cs`, `ObterGestorResponse.cs`, `ListarUsuarioResponse.cs`), todas na feature **Gestor** (não espalhadas pelas features de cada entidade), porque são produto dos próprios casos de uso do `IGestorUseCase` — é o Gestor que cria Aluno/Instrutor/Funcionário, não essas features criando a si mesmas.

## Alternativas consideradas

Para o problema da classe wrapper, três opções foram avaliadas explicitamente:

1. **Uma classe por arquivo, tudo dentro de `Gestor/Response`** — escolhida. Mantém a superfície de saída do Gestor localizável num único lugar, sem o wrapper artificial.
2. **Espalhar cada response para a pasta da feature do tipo que ela representa** (ex: `CriarAlunoResponse` → `Usuarios/Aluno/Response`) — era a sugestão original do checklist, mas descartada após reflexão: espalharia a API de saída de um único caso de uso (`IGestorUseCase`) por quatro pastas de features diferentes, dificultando responder "o que o GestorController retorna" sem procurar em vários lugares.
3. **Só corrigir o typo, manter o wrapper** — descartada; não resolve o problema de granularidade, só o nome.

## Consequências

- Interfaces de repositório agora têm nome de arquivo e nome de tipo idênticos em toda a base — convenção documentada explicitamente em `docs/Arquitetura_backend.md` ("Nome do arquivo = nome do tipo principal").
- `Usuarios/Gestor/Response/` ficou com 9 arquivos pequenos em vez de 1 arquivo grande — mais arquivos para navegar, mas cada um trivial de entender isoladamente, e cada `class` sem aninhamento pode ser testada/referenciada diretamente sem qualificar pelo nome do wrapper.
- Esse "uma classe por arquivo, sem wrapper" foi documentado como o padrão recomendado para features novas — `AlunoRP.cs`/`UsuarioRP.cs` ainda usam o padrão antigo de wrapper e não foram migrados nesta revisão (ver seção Response do documento de arquitetura).
