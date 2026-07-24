# 0007 — Eliminar a duplicação da classe `Constructor`

## Status

Implementado.

## Contexto

`Application/compartilhado/UserModels.cs` continha uma classe `Constructor` "guarda-chuva" com cópias aninhadas — campo por campo, construtor por construtor idênticas — de seis classes que também existiam de forma independente, uma por feature: `ConstructorUsuario.cs`, `ConstructorAluno.cs`, `ConstructorInstrutor.cs`, `ConstructorFuncionario.cs`, `ConstructorGestor.cs` e `ConstructorFichaMedica.cs`, cada uma na pasta `Constructor/` da sua própria feature.

O problema não era só duplicação: **era a cópia errada que estava em uso**. Seis arquivos (`IFichaMedicaUseCase.cs`, `FichaMedicaUC.cs`, `FichaMedicaController.cs`, `IGestorUseCase.cs`, `GestorUC.cs`, `GestorController.cs`) importavam os tipos via `using static Application.Compartilhado.Constructor;`, apontando para a cópia aninhada em `UserModels.cs`. Os seis arquivos individuais por feature existiam no disco, mas não eram referenciados em lugar nenhum — código morto silencioso, sem warning de compilador porque tecnicamente compilava e funcionava (só que através do caminho errado).

## Decisão

Antes de qualquer mudança, cada uma das 6 classes na cópia aninhada foi comparada campo a campo com sua contraparte por feature para confirmar equivalência exata — nenhuma mudança de comportamento fica escondida numa refatoração puramente estrutural.

Confirmada a equivalência: `UserModels.cs` foi apagado. Os 6 arquivos consumidores foram atualizados de `using static Application.Compartilhado.Constructor;` para `using` direto das pastas `Constructor` de cada feature — `IFichaMedicaUseCase.cs`/`FichaMedicaUC.cs`/`FichaMedicaController.cs` só precisavam de `Application.Fichas.FichaMedica.Constructor` (usam só `ConstructorFichaMedica`); `IGestorUseCase.cs`/`GestorUC.cs`/`GestorController.cs` precisavam das 5 restantes (`Common`, `Aluno`, `Instrutor`, `Funcionario`, `Gestor`), porque `IGestorUseCase` tem um método `CriarX` para cada um desses tipos.

## Alternativas consideradas

- **Manter `UserModels.cs` e apagar os 6 arquivos por feature** — rejeitada. Contradiria a decisão já tomada nos ADRs 0001–0004 de que cada feature é dona dos seus próprios tipos; manter um arquivo `Compartilhado` com construtores de 5 features diferentes reintroduziria o acoplamento cruzado que o resto da revisão eliminou.
- **Manter os dois e escolher caso a caso qual usar** — nunca foi uma opção real; duas fontes de verdade para o mesmo tipo é o próprio problema, não uma solução.

## Consequências

- Não existe mais nenhuma referência a `Application.Compartilhado.Constructor` ou a `UserModels.cs` no código — verificado por busca textual após a mudança.
- As classes `Constructor*` por feature (que já existiam, mas estavam órfãs) agora são as únicas em uso, consistente com a convenção de organização por feature documentada no ADR 0003.
- `docs/Arquitetura_backend.md` não menciona mais essa pendência nas seções Constructor e Compartilhado — o texto foi atualizado para refletir apenas o estado atual, sem histórico de pendência resolvida poluindo a leitura.
