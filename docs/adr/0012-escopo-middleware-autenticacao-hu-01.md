# 0012 — Escopo de "middleware" para a HU-01 (autenticação da API)

## Status

Aceito. Nenhuma classe de middleware nova foi criada.

## Contexto

A HU-01 define:

> **HU-01 — Implementar Middleware de Autenticação da API**
> Como sistema, quero validar automaticamente as credenciais dos usuários nas
> requisições da API, para garantir que apenas usuários autenticados acessem
> recursos protegidos.
>
> O sistema deverá possuir um middleware responsável por interceptar as
> requisições direcionadas aos recursos protegidos da API, realizando a
> validação das credenciais de acesso antes da execução das operações.

Não existe, em nenhum outro lugar do repositório (código, `docs/`, histórico de
commits), uma definição mais detalhada do que "middleware" significa aqui — o
texto acima é tudo o que a HU-01 especifica. Isso deixava em aberto se a tarefa
exigia implementar uma classe `IMiddleware` própria, ou se o pipeline já
existente satisfazia o requisito.

Levantamento do estado atual do projeto (`src/API/Program.cs`):

- `builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(...)`
  (linhas 91–106) valida assinatura, issuer, audience e expiração do JWT.
- `builder.Services.AddAuthorization()` (linha 108) habilita a checagem de
  `[Authorize]`, mas **sem `FallbackPolicy`** — nenhuma política padrão é
  aplicada a endpoints que não têm o atributo.
- `app.UseAuthentication()` → `app.UseAuthorization()` → `app.MapControllers()`
  (linhas 132–134), na ordem correta recomendada pela Microsoft: autenticação
  (identifica quem é o usuário) antes de autorização (decide se ele pode
  acessar o recurso), ambas antes do controller ser executado.
- Autorização por recurso é aplicada via `[Authorize]` / `[Authorize(Roles = "...")]`
  em controllers/actions (`MedicalRecordController`, `ManagerController`,
  `MemberController`, `UserController`), com `[AllowAnonymous]` explícito só em
  `login` e `refresh`.
- Não existe, e nunca existiu no histórico de commits, nenhuma classe que
  implemente `IMiddleware`, nem `app.Use((context, next) => ...)` customizado.
  Toda a interceptação de requisição hoje é feita pelos middlewares nativos do
  framework.

Ou seja: "interceptar as requisições direcionadas aos recursos protegidos" e
"validar as credenciais de acesso antes da execução das operações" — a redação
da HU-01 — descreve literalmente o que `UseAuthentication` (valida o JWT) e
`UseAuthorization` (bloqueia antes do controller rodar, com base em
`[Authorize]`) já fazem.

## Decisão

**HU-01 é satisfeita endurecendo o pipeline nativo do ASP.NET Core — não exige
uma classe `IMiddleware` customizada.**

O trabalho de HU-01 se resume a:

1. Manter `UseAuthentication`/`UseAuthorization` como estão (já corretos).
2. Fechar a lacuna de "fail-open" encontrada durante o levantamento: hoje,
   qualquer endpoint novo que um desenvolvedor esqueça de marcar com
   `[Authorize]` fica **público por padrão**, porque `AddAuthorization()` não
   define uma `FallbackPolicy`. Isso não atende ao espírito de "garantir que
   apenas usuários autenticados acessem recursos protegidos" — a segurança
   deveria ser opt-out (fechado por padrão, `[AllowAnonymous]` explícito para
   abrir), não opt-in (aberto por padrão, `[Authorize]` explícito para
   fechar). Recomenda-se configurar uma `FallbackPolicy` exigindo usuário
   autenticado por padrão, mantendo `[AllowAnonymous]` nos poucos endpoints
   que já o usam (`login`, `refresh`).
3. Nenhuma pasta `Middleware/`, nenhuma classe `IMiddleware`, nenhum
   `app.Use(...)` customizado deve ser criado para cumprir esta HU.

## Alternativas consideradas

- **Criar um `IMiddleware` customizado que reimplementa a validação do JWT
  manualmente** (parsing do header `Authorization`, verificação de assinatura
  e expiração) — rejeitada. Duplicaria exatamente o que `AddJwtBearer` já faz,
  com mais código para manter e mais superfície para erros de implementação em
  uma área sensível (validação criptográfica de token).
- **Criar um `IMiddleware` customizado posicionado antes de `UseAuthorization`
  só para logging/auditoria de acesso** — rejeitada por ora. A HU-01, como
  escrita, não pede auditoria, correlação de requisições, nem tratamento de
  erro diferenciado — só bloqueio de credencial inválida. Adicionar um
  middleware para isso seria escopo não pedido pela história.
- **Não mexer em nada, manter `AddAuthorization()` sem `FallbackPolicy`** —
  rejeitada. Deixaria a HU-01 tecnicamente "implementada" no papel (pipeline
  existe, `[Authorize]` funciona nos endpoints que o têm), mas sem garantir a
  parte de "garantir que apenas usuários autenticados acessem recursos
  protegidos" para endpoints futuros — a garantia dependeria de disciplina
  manual, não de configuração.

## Consequências

- Tasks subsequentes ligadas a HU-01 não devem criar uma classe de middleware
  nova; qualquer menção a "middleware" nelas se refere ao pipeline
  `UseAuthentication`/`UseAuthorization` já configurado em `Program.cs`.
- Falta implementar a `FallbackPolicy` fail-safe (item 2 da Decisão) como
  tarefa concreta de hardening — pendência explícita desta ADR, não
  implementada aqui.
- Se um requisito futuro pedir algo que `[Authorize]`/policies não resolvem
  sozinhos — auditoria centralizada de acesso, correlation-id por requisição,
  tratamento uniforme de exceções, rate limiting — isso justifica um
  `IMiddleware` customizado novo, e deve vir com seu próprio ADR. Este
  documento não fecha essa porta, só estabelece que a HU-01, do jeito que está
  escrita hoje, não é esse caso.
