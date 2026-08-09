# Autenticação da API — Vitalitas

> Este documento explica **como o login e o acesso protegido funcionam** no backend do Vitalitas, em linguagem simples, com os detalhes técnicos exatos logo em seguida para quem for implementar a integração (front-end, mobile, etc.). Para instruções de como rodar o projeto, veja o [README](../README.md). Para entender a organização geral do backend, veja [`Arquitetura_backend.md`](Arquitetura_backend.md).

---

## 1. A ideia, em palavras simples

Pensa no acesso ao sistema como um **crachá temporário de visitante**:

1. Você faz login (mostra usuário e senha na recepção).
2. Recebe um **crachá** (o *access token*) que te dá acesso pelas próximas **15 minutos**.
3. Junto com o crachá, você também recebe um **comprovante de retirada** (o *refresh token*), válido por **7 dias**, que serve só pra uma coisa: pedir um crachá novo quando o atual vencer, sem precisar mostrar usuário e senha de novo.
4. Quando o crachá vence, o sistema te barra (**erro 401**). Você mostra o comprovante, ganha um crachá novo (e um comprovante novo — o antigo é cancelado na hora), e segue normalmente.
5. Se o comprovante também já tiver vencido (ou já tiver sido trocado antes), você precisa voltar pra recepção e fazer login de novo.

Esse "crachá" técnico se chama **JWT (JSON Web Token)**. Ele não fica guardado em lugar nenhum no servidor — é assinado digitalmente, e o sistema confirma que é válido só de olhar pra ele. Já o "comprovante" (*refresh token*) fica registrado no banco de dados, porque ele pode ser cancelado a qualquer momento.

---

## 2. O fluxo, passo a passo

1. O front-end faz login e recebe `Token` (o crachá/access token) e `RefreshToken` (o comprovante).
2. Toda vez que o front-end pede algo protegido ao backend, ele manda o crachá junto, no cabeçalho `Authorization: Bearer <Token>`.
3. Quando o crachá vence, o backend responde **HTTP 401**.
4. O front-end então chama `POST /usuario/refresh`, mandando o crachá vencido e o comprovante.
5. O comprovante é de **uso único**: a cada troca, o antigo é cancelado e um par novo (crachá + comprovante) é emitido.
6. Se o comprovante também estiver vencido ou já tiver sido usado, o backend responde **401** de novo — e aí o front-end deve limpar tudo e mandar a pessoa pra tela de login.

---

## 3. Referência técnica dos endpoints

### `POST /usuario/login`

Público — não precisa estar logado pra chamar.

**O que enviar:**

```json
{
  "Email": "usuario@exemplo.com",
  "Senha": "suasenha"
}
```

**O que volta (200 OK):**

```json
{
  "TipoUsuario": 2,
  "IdUsuario": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Flag": true,
  "Token": "<jwt-access-token>",
  "RefreshToken": "<128-char-hex-string>",
  "Status": {
    "Message": "Login realizado com sucesso",
    "Code": 200,
    "Sucess": true
  }
}
```

| Campo | O que é |
|---|---|
| `Token` | O crachá (access token). Usar no cabeçalho `Authorization: Bearer <Token>` |
| `RefreshToken` | O comprovante, pra pedir um crachá novo em `POST /usuario/refresh` |
| `TipoUsuario` | O perfil da pessoa: 1 = Instrutor, 2 = Aluno, 3 = Gestor, 4 = Administrador |
| `IdUsuario` | Identificador único de quem fez login |
| `Flag` | Campo que o sistema devolve, mas cujo significado de negócio ainda não está documentado — confirmar com o time de backend antes de usar |
| `Status` | Como foi a operação (`Message`, `Code`, `Sucess`) |

> **Atenção a um detalhe de digitação:** o campo se chama `Sucess` (faltando um "c"), não `Success`. É assim que a API realmente devolve — use exatamente como está.

**Se der errado (401 Unauthorized):**

```json
{ "message": "Credenciais inválidas" }
```

**Exemplo em PowerShell:**

```powershell
$body = '{ "Email": "usuario@exemplo.com", "Senha": "suasenha" }'
$response = Invoke-RestMethod `
    -Uri "https://localhost:7214/usuario/login" `
    -Method POST -ContentType "application/json" -Body $body

$accessToken  = $response.Token
$refreshToken = $response.RefreshToken
```

---

### `POST /usuario/refresh`

Público — não precisa mandar o crachá no cabeçalho aqui. Recebe o crachá (mesmo vencido) e o comprovante no corpo da requisição, devolve um par novo.

**O que enviar:**

```json
{
  "AccessToken": "<jwt-expirado-ou-ainda-valido>",
  "RefreshToken": "<128-char-hex-raw-token>"
}
```

**O que volta (200 OK):**

```json
{
  "AccessToken": "<novo-jwt>",
  "RefreshToken": "<novo-128-char-hex-string>",
  "Status": {
    "Message": "Token renovado com sucesso",
    "Code": 200,
    "Sucess": true
  }
}
```

> **Importante:** depois de cada renovação, o comprovante anterior é **cancelado pra sempre**. O front-end precisa guardar imediatamente os novos valores (`AccessToken` e `RefreshToken`) recebidos aqui — o par antigo não serve mais.

**Se der errado (401 Unauthorized) — token inválido, vencido ou já usado:**

```json
{ "message": "Token inválido ou expirado" }
```

**Exemplo em PowerShell:**

```powershell
$body = @{ AccessToken = $accessToken; RefreshToken = $refreshToken } | ConvertTo-Json
$renewed = Invoke-RestMethod `
    -Uri "https://localhost:7214/usuario/refresh" `
    -Method POST -ContentType "application/json" -Body $body

$accessToken  = $renewed.AccessToken
$refreshToken = $renewed.RefreshToken
```

---

### Chamando endpoints protegidos

Em **todo** endpoint que exige login, manda o crachá no cabeçalho:

```http
Authorization: Bearer <Token>
```

**Exemplo em PowerShell:**

```powershell
$headers = @{ Authorization = "Bearer $accessToken" }
Invoke-RestMethod -Uri "https://localhost:7214/aluno/listar-aluno?IdAluno=<guid>" -Method GET -Headers $headers
```

---

## 4. Como o front-end deve se comportar

- Guardar `Token` e `RefreshToken` depois do login.
- Mandar `Authorization: Bearer <Token>` automaticamente em toda requisição protegida (idealmente via um interceptor HTTP, não manualmente em cada chamada).
- Ao receber **401** numa requisição protegida, chamar `POST /usuario/refresh`.
  - Se der certo: salvar o novo `AccessToken` e `RefreshToken`, e repetir a requisição original com o token novo.
  - Se der 401 de novo: limpar os tokens guardados e mandar a pessoa pra tela de login.
- **Nunca disparar duas renovações ao mesmo tempo.** Como o comprovante é de uso único, se duas chamadas de refresh saírem em paralelo, a segunda vai falhar (porque a primeira já cancelou o comprovante). Só deve existir **uma** renovação em andamento por vez; as demais requisições que caírem em 401 nesse meio-tempo devem esperar o resultado dessa única renovação.

**Como isso fica em pseudocódigo:**

```
ao receber 401:
  se já não estou renovando:
    marcar "renovando = verdadeiro"
    tentar:
      [tokenNovo, refreshNovo] = POST /usuario/refresh
      salvar os tokens novos
      reenviar todas as requisições que estavam esperando, com o token novo
    se der 401 de novo:
      limpar tokens → mandar pra tela de login
    ao final:
      marcar "renovando = falso"
  senão:
    colocar essa requisição numa fila → esperar a renovação em andamento terminar
```

---

## 5. 401 ou 403? Qual a diferença

| Código | O que significa | O que o front-end deve fazer |
|---|---|---|
| **401** | Não tem crachá, ou ele é inválido/venceu | Chamar `/usuario/refresh`; se falhar, mandar pra tela de login |
| **403** | O crachá é válido, mas essa pessoa não tem permissão pra essa ação específica | Mostrar mensagem de acesso negado — **não** tentar renovar o token, porque o problema não é o token |

---

## 6. Perfis de acesso (Roles)

O campo `TipoUsuario` (número) que volta no login e o campo `Role` (texto) que fica dentro do crachá representam a mesma coisa, de duas formas diferentes. O backend usa o `Role` (texto) pra decidir o que cada pessoa pode fazer; o front-end pode usar o `TipoUsuario` (número) do login pra decidir o que mostrar na tela.

| `TipoUsuario` (número) | `Role` no crachá | O que essa pessoa acessa |
|---|---|---|
| 1 | `Instrutor` | Endpoints gerais, de qualquer pessoa logada |
| 2 | `Aluno` | Endpoints de aluno, só sobre os próprios dados |
| 3 | `Gestor` | Gestão de alunos, instrutores e fichas médicas |
| 4 | `Administrador` | Acesso administrativo completo |

---

## 7. O que ainda falta / pontos em aberto

**Não existe um botão de "sair" no backend ainda:**
Hoje não há um endpoint pra cancelar o comprovante (*refresh token*) sob demanda. "Sair" no front-end, por enquanto, é só apagar os tokens guardados localmente — o comprovante continua válido no banco até vencer sozinho (7 dias). Um endpoint de logout de verdade ainda precisa ser feito.

**O campo `Flag` no login:**
Esse campo (verdadeiro/falso) vem do cadastro do usuário, mas seu significado de negócio não está documentado em lugar nenhum ainda. Confirmar com o time de backend antes de usar pra alguma decisão importante no front-end.

**Coisas que ainda precisam de combinado entre front-end e backend:**

| Assunto | O que falta decidir |
|---|---|
| Onde guardar os tokens no front-end | Memória do JavaScript (mais seguro) vs `localStorage` vs `sessionStorage` |
| Como ler o perfil da pessoa | Usar o número `TipoUsuario` do login, ou ler o `Role` de dentro do crachá — hoje dá pra fazer dos dois jeitos, falta escolher um padrão |
| Significado do campo `Flag` | Confirmar com o backend |
| Botão de "sair" de verdade | Ainda não existe no backend; falta combinar prazo |
| CORS (quem pode chamar a API) | Hoje só libera `http://localhost:3000`; confirmar se é o endereço certo do front-end |
