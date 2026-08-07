# Autenticação da API

> Documentação detalhada do fluxo de login/refresh. Para instruções de como rodar o projeto, veja o [README](../README.md).

O sistema utiliza **JSON Web Tokens (JWT)** com **refresh token** para autenticação e renovação de sessão. O access token expira em **15 minutos**; o refresh token, em **7 dias**.

## Visão Geral do Fluxo

1. O front-end realiza login e recebe `Token` (access token JWT) e `RefreshToken`.
2. Toda requisição protegida deve enviar o header `Authorization: Bearer <Token>`.
3. Quando o access token expira, o back-end retorna **HTTP 401**.
4. O front-end chama `POST /usuario/refresh` com o access token expirado e o refresh token.
5. O refresh token é **rotacionado a cada renovação**: o token anterior é revogado e um novo par é emitido.
6. Se o refresh token estiver expirado ou revogado, o back-end retorna **HTTP 401** — o front-end deve limpar os tokens e redirecionar para o login.

---

## POST /usuario/login

Público — não requer autenticação.

**Request body:**

```json
{
  "Email": "usuario@exemplo.com",
  "Senha": "suasenha"
}
```

**Response (200 OK):**

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

| Campo | Tipo | Descrição |
|---|---|---|
| `Token` | string (JWT) | Access token — use no header `Authorization: Bearer <Token>` |
| `RefreshToken` | string (hex, 128 chars) | Refresh token — use em `POST /usuario/refresh` quando o access token expirar |
| `TipoUsuario` | int | Perfil: 1 = Instrutor, 2 = Aluno, 3 = Gestor, 4 = Administrador |
| `IdUsuario` | GUID | Identificador único do usuário autenticado |
| `Flag` | bool | Campo retornado pelo domínio; significado a confirmar com o time de back-end |
| `Status` | objeto | Resultado da operação (`Message`, `Code`, `Sucess`) |

> **Atenção:** O campo `Sucess` no objeto `Status` é retornado com este nome pela API (erro tipográfico presente no código-fonte). Utilize exatamente como retornado.

**Response (401 Unauthorized):**

```json
{ "message": "Credenciais inválidas" }
```

**Exemplo PowerShell:**

```powershell
$body = '{ "Email": "usuario@exemplo.com", "Senha": "suasenha" }'
$response = Invoke-RestMethod `
    -Uri "https://localhost:7214/usuario/login" `
    -Method POST -ContentType "application/json" -Body $body

$accessToken  = $response.Token
$refreshToken = $response.RefreshToken
```

---

## POST /usuario/refresh

Público — não requer Bearer token no header. Recebe o access token (mesmo expirado) e o refresh token no body e retorna um novo par de tokens.

**Request body:**

```json
{
  "AccessToken": "<jwt-expirado-ou-ainda-valido>",
  "RefreshToken": "<128-char-hex-raw-token>"
}
```

**Response (200 OK):**

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

> **Token rotation:** após cada renovação, o refresh token anterior é **revogado permanentemente**. O front-end deve substituir imediatamente os tokens armazenados pelos novos valores (`AccessToken` e `RefreshToken`) retornados nesta resposta.

**Response (401 Unauthorized) — token inválido, expirado ou já utilizado:**

```json
{ "message": "Token inválido ou expirado" }
```

**Exemplo PowerShell:**

```powershell
$body = @{ AccessToken = $accessToken; RefreshToken = $refreshToken } | ConvertTo-Json
$renewed = Invoke-RestMethod `
    -Uri "https://localhost:7214/usuario/refresh" `
    -Method POST -ContentType "application/json" -Body $body

$accessToken  = $renewed.AccessToken
$refreshToken = $renewed.RefreshToken
```

---

## Requisições Autenticadas

Inclua o access token no header de **todas** as requisições a endpoints protegidos:

```http
Authorization: Bearer <Token>
```

**Exemplo PowerShell:**

```powershell
$headers = @{ Authorization = "Bearer $accessToken" }
Invoke-RestMethod -Uri "https://localhost:7214/aluno/listar-aluno?IdAluno=<guid>" -Method GET -Headers $headers
```

---

## Integração com o Front-end

O front-end deve:

- Armazenar `Token` (access token) e `RefreshToken` após o login.
- Anexar `Authorization: Bearer <Token>` automaticamente em todas as requisições protegidas, via interceptor HTTP.
- Ao receber **HTTP 401** em uma requisição protegida, chamar `POST /usuario/refresh`.
  - Se o refresh for bem-sucedido: salvar o novo `AccessToken` e o novo `RefreshToken`, e reenviar a requisição original com o novo token.
  - Se o refresh falhar (HTTP 401): limpar os tokens e redirecionar o usuário para o login.
- **Evitar chamadas simultâneas ao endpoint de refresh.** O refresh token é de uso único — chamadas paralelas a `/usuario/refresh` causam falha nas subsequentes porque o token já foi rotacionado pela primeira chamada. Apenas **uma** chamada de refresh deve ocorrer por vez; as demais requisições com 401 devem aguardar o resultado.

**Pseudocódigo do interceptor:**

```
on 401 response:
  if (não estou renovando):
    marcar "renovando = true"
    try:
      [novoToken, novoRefreshToken] = POST /usuario/refresh
      salvar novos tokens
      reenviar todas as requisições pendentes com novoToken
    catch 401:
      limpar tokens → redirecionar para login
    finally:
      marcar "renovando = false"
  else:
    enfileirar requisição → aguardar resultado do refresh
```

---

## 401 vs 403

| Status HTTP | Causa no back-end | Ação do front-end |
|---|---|---|
| **401** | Token ausente, inválido ou expirado | Chamar `/usuario/refresh`; se falhar, redirecionar para login |
| **403** | Token válido, mas sem permissão para a operação | Exibir mensagem de acesso negado; **não** tentar renovar o token |

---

## Roles e Perfil de Usuário

O campo `TipoUsuario` (int) na resposta do login e o claim `Role` (string) embutido no JWT representam o mesmo valor. O back-end usa o claim `Role` para autorização; o front-end pode usar `TipoUsuario` da resposta do login para controle de navegação e exibição de UI.

| `TipoUsuario` (int) | Claim `Role` no JWT | Acesso |
|---|---|---|
| 1 | `Instrutor` | Endpoints gerais autenticados |
| 2 | `Aluno` | Endpoints de aluno, com restrição aos próprios dados |
| 3 | `Gestor` | Gestão de alunos, instrutores e fichas médicas |
| 4 | `Administrador` | Acesso administrativo completo |

---

## Limitações Atuais e Pontos Pendentes

**Sem endpoint de logout no back-end:**
Não existe um endpoint para revogar o refresh token sob demanda. O logout no front-end deve ser feito **limpando os tokens armazenados localmente**. O refresh token no banco de dados permanece válido até sua expiração natural (7 dias). Um endpoint de revogação está pendente de implementação no back-end.

**Campo `Flag` na resposta do login:**
O campo `Flag` (bool) pertence à entidade de domínio `Usuario` e é retornado no login. Seu significado de negócio não está documentado no código-fonte e deve ser confirmado com o time de back-end antes de ser utilizado pelo front-end.

**Alinhamentos pendentes com o front-end:**

| Ponto | Decisão necessária |
|---|---|
| Armazenamento dos tokens | Memória JS (mais seguro) vs `localStorage` vs `sessionStorage` |
| Leitura de perfil/role | Usar `TipoUsuario` int da resposta do login ou decodificar o claim `Role` do JWT — definir um padrão |
| Significado do campo `Flag` | Confirmar com o back-end |
| Endpoint de logout/revogação | Pendente de implementação no back-end; alinhar prazo |
| CORS | Back-end libera apenas `http://localhost:3000`; confirmar origem do front-end |
