# 0002 — Reorganizar a Infrastructure por feature

## Status

Implementado.

## Contexto

O commit `ede963b` afirmava ter revisado a pasta Infrastructure, mas na prática `src/Infrastructure/Repositories` continuava sendo uma pasta única e flat (`AlunoRepository.cs`, `UsuarioRepository.cs`, `GestorRepository.cs`, `FichaMedicaRepository.cs`, `RefreshTokenRepository.cs`, todos soltos no mesmo nível). Era a única camada das quatro (API, Application, Domain, Infrastructure) que não seguia o padrão "pasta por feature" já adotado no resto do projeto, e contradizia o próprio exemplo do `docs/Arquitetura_backend.md`, que já mostrava `Repositories/Usuarios/AlunoRepository.cs` como estrutura esperada.

## Decisão

Cada repositório foi movido para uma subpasta espelhando exatamente onde a interface `Domain.Features.*.Interfaces.I*Repository` correspondente vive no Domain:

```
Repositories/Usuarios/Aluno/AlunoRepository.cs      → implementa IAlunoRepository (Usuarios/Aluno/Interfaces)
Repositories/Usuarios/Common/UsuarioRepository.cs   → implementa IUsuarioRepository (Usuarios/Common/Interfaces)
Repositories/Usuarios/Gestor/GestorRepository.cs    → implementa IGestorRepository (Usuarios/Gestor/Interfaces)
Repositories/Fichas/FichaMedica/FichaMedicaRepository.cs
Repositories/Token/RefreshTokenRepository.cs
```

O ponto que exigiu uma regra explícita: `UsuarioRepository` foi para `Usuarios/Common`, não `Usuarios` direto — porque a interface que ele implementa (`IUsuarioRepository`) está em `Domain/Features/Usuarios/Common/Interfaces`, não em um nível "Usuarios" genérico. A regra fixada foi: **o subcaminho do repositório espelha onde a interface correspondente está no Domain, não o nome da classe.**

Os namespaces foram atualizados junto (ex: `Infrastructure.Repositories` → `Infrastructure.Repositories.Usuarios.Common`), reaplicando a mesma técnica de qualificação completa do ADR 0001 nos dois novos pontos de colisão que apareceram (`FichaMedicaRepository` referenciando `FichaMedica`, `GestorRepository` referenciando `Aluno`).

## Alternativas consideradas

Nenhuma alternativa de estrutura foi seriamente considerada — a decisão de espelhar `Domain/Features` já estava implícita no exemplo pré-existente do `docs/Arquitetura_backend.md` e é consistente com o padrão já usado em Application e Domain. A única decisão real foi onde colocar `UsuarioRepository` (`Usuarios/Common` vs. `Usuarios` direto), resolvida pela regra acima.

## Consequências

- As quatro camadas agora seguem exatamente o mesmo padrão de pastas — um desenvolvedor que sabe onde está `Domain/Features/Usuarios/Aluno` sabe onde procurar `Infrastructure/Repositories/Usuarios/Aluno`.
- `Program.cs` (posteriormente movido para `Infrastructure/Extensions/InfrastructureServiceExtensions.cs` no ADR 0005) precisou ser atualizado para os novos namespaces totalmente qualificados de cada repositório.
- Se uma feature nova precisar de repositório e a interface do Domain estiver num subnível não óbvio (como `Common`), a localização do repositório deve seguir a interface, não intuição sobre o nome da entidade.
