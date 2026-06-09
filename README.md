# Vitalitas - Backend API

> **API RESTful para gestão integrada de academias.**

![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-yellow?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-0089D6?style=for-the-badge&logo=microsoft-azure&logoColor=white)

> ℹ️ **Visão Geral:** Para entender o contexto acadêmico, a proposta de valor e o escopo do produto (MVP), acesse o **[README da Organização Vitalitas](https://github.com/VitalitasPJT)**.

## Arquitetura e Design

> 🚧 **EM REVISÃO ACADÊMICA:** Os diagramas e decisões arquiteturais abaixo estão em fase de validação pelos orientadores do projeto e podem sofrer alterações.

O backend foi desenvolvido seguindo princípios de **Clean Architecture** e **Domain-Driven Design (DDD)** simplificado, visando desacoplamento entre as regras de negócio e a infraestrutura.

### Visão da Solução
A API atua como o núcleo central do sistema, operando de forma *stateless* e servindo os clientes web/mobile.

<!-- ![Diagrama de Arquitetura](./arquitetura.png) -->
🚧 Diagrama em desenvolvimento 🚧
*(Fluxo: React Client ↔ API .NET Core ↔ SQL Server / Azure Services)*

### Modelagem de Dados
A estrutura relacional foi projetada no **SQL Server** para garantir a integridade de dados críticos como fichas médicas e histórico de treinos.

<!-- ![Diagrama Entidade Relacionamento](./der_database.png) -->
🚧 Diagrama em desenvolvimento 🚧
*(Principais entidades: Usuários, Perfis, Treinos, Fichas e Avaliações)*

### Infraestrutura
O projeto utiliza a nuvem da **Microsoft Azure**:
* **App Service:** Hospedagem da API.
* **Azure SQL Database:** Persistência dos dados.

## Configuração do Ambiente de Desenvolvimento

Siga este guia para configurar o ambiente local, o banco de dados e as credenciais de segurança.

### Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas:
* **[.NET SDK 9.0+](https://dotnet.microsoft.com/download)**
* **[SQL Server 2022 Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)** 
* **[SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)** 

### 1. Configuração do Banco de Dados
1.  **Primeiramente:** Após baixar o SQL Server 2022 e o SQL Server Management Studio (SSMS), certifique-se de que você possui o LocalDB em sua máquina. Para isso, abra o PowerShell:

```bash
sqllocaldb info
```

Se aparecer MSSQLLocalDB, o LocalDB está funcionando corretamente.

1.2 **Caso não apareça:** Instale o Visual Studio Community 2026 (ou a versão que aparecer dentro do seu VS) e certifique-se de que ativou a opção LocalDB na área de componentes individuais.

2.  **Conexão Inicial:** Abra o SSMS e conecte-se à sua instância local `(localdb)\MSSQLLocalDB` ou `.\SQLEXPRESS`.

3.  **Configuração de Usuário:**
    * Crie um novo Login com **Autenticação SQL** (não use apenas a do Windows).
    * Desmarque a opção *"Impor política de senha"* para facilitar o desenvolvimento.
    * Defina o banco de dados padrão como `master` e garanta que a função de servidor `public` esteja marcada.

4.  **Criação do Banco:**
    * Crie um novo banco de dados com o nome: `[VITALITAS_DEV]`.
    * Defina o "Proprietário" (Owner) como o usuário que você acabou de criar.

5.  **Carga de Dados:**
    * No SSMS, abra o menu superior esquerdo, selecione o arquivo e execute os scripts:
        `vitalitas-backend\src\Infrastructure\Database\CREATE.sql`
    * Em seguida, abra e execute o script de população de dados:
        `vitalitas-backend\src\Infrastructure\Database\INSERT.sql`

> **Nota:** Verifique no *SQL Server Configuration Manager* se o protocolo **TCP/IP** está habilitado para o SQLEXPRESS.

### 2. Configuração da Aplicação (Backend)

#### Segredos do Usuário (User Secrets)
Para garantir a segurança da chave de assinatura do Token JWT, não a armazenamos no código fonte. Utilizamos o recurso de **User Secrets**.

Abra o terminal na pasta raiz do projeto (`vitalitas-backend`) e execute:

```bash
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_AQUI"
```

#### String de Conexão
Abra o arquivo `appsettings.json` e localize a seção ConnectionStrings. Atualize o parâmetro `DefaultConnection` com as credenciais do usuário que você criou no passo 1 :

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=VITALITAS_DEV;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;"
}
```

### 3. Executando a Aplicação
Com o banco configurado e as chaves definidas, execute os comandos abaixo no terminal dentro da pasta do projeto:

```bash
dotnet restore
dotnet build
dotnet run
```

A API estará disponível em `https://localhost:7214` (HTTPS) ou `http://localhost:5156` (HTTP), conforme configurado em `launchSettings.json`.

## Documentação da API

A API segue o padrão RESTful e sua documentação interativa é gerada automaticamente via **Swagger/OpenAPI**.

### Acesso ao Swagger
Após iniciar a aplicação localmente, a documentação completa dos endpoints, esquemas de requisição e tipos de resposta estará disponível em:

> **https://localhost:7214/swagger** (HTTP: http://localhost:5156/swagger)

### Autenticação e Segurança

O sistema utiliza **JSON Web Tokens (JWT)** com **refresh token** para autenticação e renovação de sessão. O access token expira em **15 minutos**; o refresh token, em **7 dias**.

#### Visão Geral do Fluxo

1. O front-end realiza login e recebe `Token` (access token JWT) e `RefreshToken`.
2. Toda requisição protegida deve enviar o header `Authorization: Bearer <Token>`.
3. Quando o access token expira, o back-end retorna **HTTP 401**.
4. O front-end chama `POST /usuario/refresh` com o access token expirado e o refresh token.
5. O refresh token é **rotacionado a cada renovação**: o token anterior é revogado e um novo par é emitido.
6. Se o refresh token estiver expirado ou revogado, o back-end retorna **HTTP 401** — o front-end deve limpar os tokens e redirecionar para o login.

---

#### POST /usuario/login

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

#### POST /usuario/refresh

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

#### Requisições Autenticadas

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

#### Integração com o Front-end

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

#### 401 vs 403

| Status HTTP | Causa no back-end | Ação do front-end |
|---|---|---|
| **401** | Token ausente, inválido ou expirado | Chamar `/usuario/refresh`; se falhar, redirecionar para login |
| **403** | Token válido, mas sem permissão para a operação | Exibir mensagem de acesso negado; **não** tentar renovar o token |

---

#### Roles e Perfil de Usuário

O campo `TipoUsuario` (int) na resposta do login e o claim `Role` (string) embutido no JWT representam o mesmo valor. O back-end usa o claim `Role` para autorização; o front-end pode usar `TipoUsuario` da resposta do login para controle de navegação e exibição de UI.

| `TipoUsuario` (int) | Claim `Role` no JWT | Acesso |
|---|---|---|
| 1 | `Instrutor` | Endpoints gerais autenticados |
| 2 | `Aluno` | Endpoints de aluno, com restrição aos próprios dados |
| 3 | `Gestor` | Gestão de alunos, instrutores e fichas médicas |
| 4 | `Administrador` | Acesso administrativo completo |

---

#### Limitações Atuais e Pontos Pendentes

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

## Equipe Backend

Responsáveis pela arquitetura, banco de dados e regras de negócio:

* **Sanderson Machado** - *Gerente de Projeto / Tech Lead*
    * **Foco:** Arquitetura Backend, Definição de Backlog (PO) e Liderança Técnica.
   * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/sandersonnexum) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](https://github.com/sandersonnexum)

* **Hugo Matos** - *DBA / QA*
    * **Foco:** Modelagem de dados (DER/MER), Scripts SQL e Testes de Qualidade.
    * [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](https://github.com/HugoFMat)
      <!-- Adicionar linkedin do Hugo !!! Backend/ Main Repo Vitalitas [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN) -->

* **Pedro Luis de Souza Abreu** - *Desenvolvedor Back-end*
    * **Foco:** Desenvolvimento de APIs, Regras de Negócio e Integração com Banco.
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/pedro-luiz-abreu-90a849355/) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](https://github.com/Pedrolsza)

## Licença

Este projeto foi desenvolvido exclusivamente para fins acadêmicos na disciplina de **Projeto Integrador** do **Centro Universitário de Brasília (UniCEUB)**.

Copyright © 2026 **Vitalitas**. Todos os direitos reservados.
