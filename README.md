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

🚧 Diagrama em desenvolvimento 🚧

*(Fluxo: React Client ↔ API .NET Core ↔ SQL Server / Azure Services)*

### Modelagem de Dados
A estrutura relacional é gerada pelo **Entity Framework Core** (Code-First) a partir das entidades de Domain — ver [`docs/adr/0014`](docs/adr/0014-migracao-dapper-para-ef-core.md). O schema não é mais mantido manualmente via scripts SQL.

🚧 Diagrama em desenvolvimento 🚧

*(Principais entidades: Usuários, Perfis, Treinos, Fichas e Avaliações)*

### Infraestrutura
O projeto utiliza a nuvem da **Microsoft Azure**:
* **Azure SQL Database:** provisionado — usado como banco de dados de nuvem via `ConnectionStrings:ConexaoPadrao` (User Secrets), com acesso restrito por firewall a IPs liberados.
* **App Service:** Hospedagem da API — ainda **não** provisionado.
* **Key Vault:** gestão de segredos em produção — ainda **não** implementado (fora de escopo até o momento).

> ⚠️ Antes de provisionar qualquer novo recurso, confira **Cost Management + Billing** no [portal Azure](https://portal.azure.com) pra garantir que a subscription usada não tem cobrança residual de outro projeto.

### Documentação Técnica

A arquitetura do backend (Clean Architecture, organização por feature, convenções de nomenclatura) está documentada em detalhe em [`docs/Arquitetura_backend.md`](docs/Arquitetura_backend.md). As decisões estruturais tomadas durante a revisão de arquitetura — com contexto, alternativas consideradas e consequências — estão registradas como ADRs em [`docs/adr/`](docs/adr/README.md).

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

5.  **Schema e dados iniciais:** desde a migração para Entity Framework Core (ver [`docs/adr/0014`](docs/adr/0014-migracao-dapper-para-ef-core.md)), o schema não é mais criado rodando `.sql` manualmente no SSMS. Com a connection string configurada (passo 2 abaixo), basta rodar a aplicação:

    ```bash
    dotnet run --project src/API
    ```

    Em `Development`, o `Program.cs` aplica as migrations pendentes automaticamente (`dbContext.Database.Migrate()`) e popula o banco com o mínimo necessário para logar (1 Academia + 1 usuário Gestor, via `DevelopmentSeeder`) — nenhum passo manual adicional é necessário. Para aplicar as migrations sem subir a API (ex.: preparar o banco antes do primeiro `dotnet run`), use a CLI do EF Core:

    ```bash
    dotnet tool install --global dotnet-ef  # se ainda não tiver a ferramenta instalada
    dotnet ef database update --project src/Infrastructure --startup-project src/API
    ```

    Os scripts antigos (`CREATE.sql`/`INSERT.sql`) ficam arquivados em `docs/archive/sql-scripts-legado/` só como referência histórica do schema anterior — não são mais executados por nenhum processo.

> **Nota:** Verifique no *SQL Server Configuration Manager* se o protocolo **TCP/IP** está habilitado para o SQLEXPRESS.

### 2. Configuração da Aplicação (Backend)

`appsettings.json` fica versionado e **não** contém segredos de propósito (`ConnectionStrings:ConexaoPadrao` e `Jwt:Key` ficam vazios nele). Existem duas formas de suprir esses valores localmente — escolha uma:

#### Opção A — copiar o arquivo de exemplo (mais simples)

```bash
cp src/API/appsettings.Development.json.example src/API/appsettings.Development.json
```

Depois edite `src/API/appsettings.Development.json` (esse arquivo está no `.gitignore`, nunca é commitado) e preencha:
- `ConnectionStrings:ConexaoPadrao` com a string de conexão do banco criado no passo 1.
- `Jwt:Key` com qualquer string aleatória de pelo menos 32 caracteres.

#### Opção B — User Secrets

```bash
dotnet user-secrets init --project src/API
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_AQUI" --project src/API
```

A connection string ainda precisa ser definida via Opção A (ou também via `dotnet user-secrets set "ConnectionStrings:ConexaoPadrao" "..."`).

#### Opção C — Azure SQL (em vez de LocalDB)

Para rodar contra o Azure SQL Database provisionado na nuvem (em vez do banco local), defina a connection string real via User Secrets — **nunca** em `appsettings.json`:

```bash
dotnet user-secrets set "ConnectionStrings:ConexaoPadrao" "Server=tcp:<seu-servidor>.database.windows.net,1433;Database=<seu-banco>;User ID=<usuario>;Password=<senha>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;" --project src/API
```

Pré-requisitos:
- Seu IP público atual precisa estar liberado no firewall do servidor Azure SQL (**Networking > Firewall rules** no portal Azure) — não basta habilitar "Allow Azure services and resources to access this server".
- Confirme que **Public network access** está habilitado para o servidor.

Com `ASPNETCORE_ENVIRONMENT=Development`, rodar `dotnet run --project src/API` aplica as migrations pendentes e roda o `DevelopmentSeeder` automaticamente **no banco apontado pela connection string ativa** — inclusive no Azure SQL, se for esse o valor configurado. Para aplicar só o schema sem subir a API nem popular dados de seed, use `dotnet ef database update` (passo 5 acima).

> A lista completa de parâmetros de configuração — o que cada um faz, tipo, e se é obrigatório por ambiente — está na seção [Referência de Configurações](#referência-de-configurações) mais abaixo.

### 3. Executando a Aplicação
Com o banco configurado e as chaves definidas, execute os comandos abaixo no terminal dentro da pasta do projeto:

```bash
dotnet restore
dotnet build
dotnet run
```

A API estará disponível em `https://localhost:7214` (HTTPS) ou `http://localhost:5156` (HTTP), conforme configurado em `launchSettings.json`.

## Referência de Configurações

Todos os parâmetros de configuração da aplicação, de onde vêm e como sobrescrever cada um sem alterar código (variável de ambiente, User Secrets, ou arquivo por ambiente). Detalhes de implementação e decisões de design estão em [`docs/adr/0010`](docs/adr/0010-options-pattern-para-configuracao-jwt.md), [`docs/adr/0011`](docs/adr/0011-environment-variables-provider.md) e [`docs/adr/0015`](docs/adr/0015-estrategia-tres-ambientes.md) (estratégia dos três ambientes).

### Três ambientes

`appsettings.json` (base), `appsettings.Development.json`, `appsettings.Staging.json` e `appsettings.Production.json` existem para os três ambientes. Só o base e os dois últimos são versionados — todos sem segredo real, só estrutura (ver [`docs/adr/0015`](docs/adr/0015-estrategia-tres-ambientes.md)). `appsettings.Development.json` é local, criado a partir do `.example` (passo 2 acima), e nunca é commitado. Staging e Production ainda não têm infraestrutura real provisionada — os arquivos só preparam a estrutura de configuração, os valores reais serão supridos via variável de ambiente/Key Vault quando o deploy acontecer.

### Como a aplicação resolve configuração

Ordem de precedência (o de baixo sobrescreve o de cima), do jeito que o `WebApplication.CreateBuilder` já monta por padrão:

1. `appsettings.json` (versionado, sem segredos)
2. `appsettings.{ASPNETCORE_ENVIRONMENT}.json` (versionado sem segredos para Staging/Production; `appsettings.Development.json` **não** é versionado)
3. User Secrets (só quando `ASPNETCORE_ENVIRONMENT=Development`)
4. Variáveis de ambiente do processo (`Chave__Subchave`, dois underscores no lugar do `:`)
5. Argumentos de linha de comando

Em produção/staging (Azure App Service ou similar), os valores marcados como **obrigatório** abaixo devem ser fornecidos via variável de ambiente ou Key Vault — nunca em `appsettings.json`.

### Parâmetros

| Chave | Tipo | Obrigatório | Descrição | Ambiente aplicável | Variável de ambiente equivalente |
|---|---|---|---|---|---|
| `ConnectionStrings:ConexaoPadrao` | string | Sim (todos) | Connection string do SQL Server usada pelo `VitalitasDbContext` (Entity Framework Core) | Todos (valor por ambiente) | `ConnectionStrings__ConexaoPadrao` |
| `Jwt:Key` | string (≥ 32 caracteres) | Sim (todos) | Chave simétrica usada para assinar e validar tokens JWT (HMAC-SHA256). Nunca commitar o valor real. | Todos (valor **diferente** por ambiente) | `Jwt__Key` |
| `Jwt:Issuer` | string | Sim | Claim `iss` emitido no token e validado na autenticação | Todos (normalmente igual em todos) | `Jwt__Issuer` |
| `Jwt:Audience` | string | Sim | Claim `aud` emitido no token e validado na autenticação | Todos (normalmente igual em todos) | `Jwt__Audience` |
| `Jwt:DurationInMinutes` | int (> 0) | Sim | Tempo de vida do access token, em minutos | Todos (hoje `15` em todos) | `Jwt__DurationInMinutes` |
| `Jwt:RefreshTokenDurationInDays` | int (> 0) | Não (default `7`) | Tempo de vida do refresh token, em dias | Todos (hoje `7` em todos) | `Jwt__RefreshTokenDurationInDays` |
| `Logging:LogLevel:Default` | string (`Trace`\|`Debug`\|`Information`\|`Warning`\|`Error`\|`Critical`\|`None`) | Não | Verbosidade padrão de log da aplicação | Todos — recomendado `Information` em Dev, `Warning` em Staging/Prod | `Logging__LogLevel__Default` |
| `Logging:LogLevel:Microsoft.AspNetCore` | string (mesmo enum acima) | Não | Verbosidade de log específica do framework ASP.NET Core | Todos — recomendado `Warning` em todos | `Logging__LogLevel__Microsoft.AspNetCore` |
| `AllowedHosts` | string (`*` ou lista separada por `;`) | Não | Filtro de host header aceito pela aplicação | Todos — `*` é aceitável em Dev; recomendado restringir ao domínio real em Staging/Prod | `AllowedHosts` |

### Conhecido, mas ainda não parametrizado

- **Origem CORS** (`http://localhost:3000`) está hardcoded em `Program.cs`, não é um parâmetro de `appsettings.json`. Candidato natural a virar configuração (`Cors:AllowedOrigins`) quando existir um front-end de Staging/Produção com origem diferente.
- **`Tenancy`** e **`Notificações`** — seções de configuração previstas em PBIs separados, ainda não implementadas; não existem no `appsettings.json` hoje.

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
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/hugo-ferreira-matos-265b0426b?utm_source=share_via&utm_content=profile&utm_medium=member_android)

* **Pedro Luis de Souza Abreu** - *Desenvolvedor Back-end*
    * **Foco:** Desenvolvimento de APIs, Regras de Negócio e Integração com Banco.
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/pedro-luiz-abreu-90a849355/) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](https://github.com/Pedrolsza)

## Licença

Este projeto está sendo desenvolvido exclusivamente para fins acadêmicos na disciplina de **Projeto Integrador** do **Centro Universitário de Brasília (UniCEUB)** e foi atualizado completamente no 7º semestre (09 de junho 2026).

Copyright © 2026 **Vitalitas**. Todos os direitos reservados.
