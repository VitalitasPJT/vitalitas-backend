# 0013 — Migração para `AuthorizationPolicy` nomeadas

## Status

Implementado.

## Contexto

A autorização por role vivia solta em `[Authorize(Roles = "Gestor,Administrador")]`
espalhado por controller/action, com a string de roles repetida e digitada à mão em
cada lugar. Dois problemas concretos vieram dessa forma:

1. **Risco de digitação/esquecimento** — já demonstrado pelo achado da auditoria de
   endpoints (task anterior): bastava um desenvolvedor esquecer o atributo, ou
   digitar `"Administardor"` em vez de `"Administrador"`, para o endpoint ficar
   liberado sem ninguém perceber em code review.
2. **Divergência entre código e regra de negócio catalogada** — ao cruzar a
   "Matriz de permissões por funcionalidade" (fornecida pelo time de negócio, com
   todas as ações já separadas por perfil: Gestor, Admin, Instrutor, Aluno) contra
   o código atual, dois `[Authorize(Roles=...)]` estavam **errados**, não só mal
   nomeados:
   - `MedicalRecordController` (ação `CriarFichaMedica`) exigia
     `"Gestor,Administrador"`. A matriz diz que "Ficha médica base — editar" é
     **exclusiva do Instrutor** — Gestor e Administrador aparecem com ✗ nessa
     linha.
   - `ManagerController.CriarInstrutor` exigia `"Gestor,Administrador"`. A matriz
     diz que "Criar Instrutores" é **exclusivo do Gestor** — Administrador
     aparece com ✗.
   - `ManagerController.CriarUsuario` aceita um campo `TipoUsuario` arbitrário no
     corpo da requisição e, com a mesma policy de classe `"Gestor,Administrador"`,
     permitia que um Administrador criasse um usuário com
     `TipoUsuario = Administrador` (ou até `Gestor`) — uma escalação de
     privilégio real. A matriz diz "Criar Administradores: Admin ✗".

A matriz também deixa claro que várias permissões têm qualificador de escopo
("✓ todos" vs "✓ alunos", "✓ próprio", "✓ dos alunos", "✓ ao prescrever") que uma
`AuthorizationPolicy` declarativa não resolve sozinha — esses casos continuam
exigindo checagem imperativa do dono do recurso dentro da action (o padrão que já
existia em `MemberController`/`UserController` via `User.FindFirst("IdUsuario")`).

## Decisão

Criada `API/Authorization/AuthorizationPolicies.cs` com constantes nomeadas,
registradas centralmente em `Program.cs` via `AddAuthorization`, uma por
capacidade de negócio (não por combinação arbitrária de roles):

| Policy | Roles | Uso |
|---|---|---|
| `PodeGerenciarUsuarios` | Gestor, Administrador | `ManagerController`: `CriarUsuario` (com checagem extra abaixo), `ListarUsuarios`, `ListarUsuario` |
| `PodeGerenciarInstrutores` | Gestor | `ManagerController.CriarInstrutor` |
| `PodeGerenciarAlunos` | Gestor, Administrador | `ManagerController`: `CriarAluno`, `ListarAlunos`; `MemberController.VincularInstrutor` |
| `PodeEditarFichaMedica` | Instrutor | `MedicalRecordController` (classe inteira) |
| `PodeTrocarSenha` | Aluno | `MemberController.TrocarSenha` |
| `PodeAtualizarObjetivoAluno` | Aluno, Gestor, Administrador | `MemberController.AtualizarObjetivo` |

**Escopo desta migração**: só os `[Authorize(Roles=...)]` que já existiam no
código (`ManagerController`, `MedicalRecordController`, `MemberController`).
Funcionalidades da matriz que ainda não têm endpoint implementado (dashboard BI,
treino, agendamento, ficha de treino, etc.) não ganharam policy ainda — nasceriam
órfãs, sem nada para proteger. Ficam para quando esses endpoints forem
construídos, usando a matriz como referência.

`ManagerController` deixou de ter `[Authorize(Roles=...)]` a nível de classe —
cada action agora declara sua própria policy explicitamente. Antes, qualquer
action nova nessa classe herdava automaticamente `"Gestor,Administrador"` mesmo
que devesse ser mais restrita (foi exatamente esse herdar-por-omissão que
causou o erro do `CriarInstrutor`); com policy explícita por action, não tem
herança silenciosa — e o `FallbackPolicy` do ADR-0012 garante que, se alguém
esquecer, o endpoint exige autenticação (nunca fica público) em vez de aberto.

**`CriarUsuario`**: continua exigindo `PodeGerenciarUsuarios` na entrada (Gestor
ou Administrador podem chamar), mas ganhou uma checagem imperativa adicional —
se `TipoUsuario` pedido não for `Aluno`, exige role `"Gestor"` especificamente.
Isso não virou uma policy declarativa porque depende do corpo da requisição
(o mesmo motivo pelo qual os casos "✓ próprio"/"✓ alunos" da matriz também ficam
como checagem imperativa, não como `AuthorizationPolicy`).

## Alternativas consideradas

- **Resource-based authorization (`IAuthorizationHandler` com `AuthorizeAsync(User, resource, policy)`)** —
  resolveria de forma totalmente declarativa até os casos com qualificador de
  escopo ("próprio", "dos alunos"). Rejeitada por ora: nenhum desses casos hoje
  passa de uma comparação simples de `IdUsuario`, então um handler genérico
  seria infraestrutura nova para um problema que duas linhas de `if` já resolvem;
  reconsiderar se os critérios de posse ficarem mais complexos (ex.: precisar
  consultar o banco para saber o dono).
- **Manter roles soltos, só extrair para uma classe de constantes de string**
  (`RoleNames.Gestor`, etc.) — reduz o risco de digitação mas não resolve o
  problema de fundo: cada endpoint ainda decide sua própria combinação de roles
  ad-hoc, sem um nome que amarre a combinação à capacidade de negócio da matriz.
  Rejeitada.
- **Policy por role individual** (`PodeSerGestor`, `PodeSerAdministrador`,
  compostas no atributo) — mais granular, mas empurraria a lógica de "quais
  roles juntos" para o atributo do controller de novo, perdendo o ganho de
  centralizar a combinação em um único lugar nomeado. Rejeitada.

## Consequências

- Toda a autorização por role dos 4 controllers existentes está centralizada em
  `AuthorizationPolicies.cs` + o bloco `AddAuthorization` de `Program.cs` — para
  saber "quem pode chamar X", basta olhar o nome da policy no atributo, sem User
  tracing.
- Dois bugs de autorização reais foram corrigidos como parte da migração:
  `CriarInstrutor` (Admin não deveria poder) e `CriarUsuario` (Admin não deveria
  poder criar Administrador/Gestor/Instrutor via esse endpoint).
- Endpoint novo em `ManagerController`/`MemberController`/`MedicalRecordController`
  precisa escolher (ou criar) uma policy nomeada explicitamente — não há mais
  atributo de classe para "herdar por omissão". Se esquecido, cai no
  `FallbackPolicy` (autenticado, mas sem restrição de role) — mais seguro que o
  comportamento antigo, mas ainda exige revisão manual de PR para pegar o caso.
- Quando as funcionalidades pendentes da matriz (BI, treino, agendamento,
  financeiro) forem implementadas, a matriz já dá o nome e a combinação de roles
  de cada policy nova a criar.
