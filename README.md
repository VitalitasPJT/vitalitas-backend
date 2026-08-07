# Vitalitas - Backend API

> **API RESTful para gestão integrada de academias.**

![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-yellow?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET_9-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-0089D6?style=for-the-badge&logo=microsoft-azure&logoColor=white)

> ℹ️ Para o contexto de produto (proposta de valor, escopo do MVP), acesse o **[README da Organização Vitalitas](https://github.com/VitalitasPJT)**. Este README cobre só o backend: arquitetura, banco de dados e como rodar.

<div align="center">

[![Mapa Mental do Projeto](media/banner-mapa-mental.png)](media/diagramas/)

</div>

Diagramas individuais (arquitetura, DER/MER, fluxos) ficam em [`media/diagramas/`](media/diagramas).

## Arquitetura e Design

O backend segue **Clean Architecture** com quatro projetos (`Domain`, `Application`, `Infrastructure`, `API`), cada um só referenciando as camadas internas a ele — ver [`docs/Arquitetura_backend.md`](docs/Arquitetura_backend.md) e os [ADRs](docs/adr/README.md) para o histórico completo das decisões estruturais.

### Acesso a dados: Dapper (runtime) + EF Core (schema)

O backend usa **dois mecanismos deliberadamente separados**, não um ORM único:

* **Dapper** (`src/Infrastructure/Repositories/**`) é o único caminho de leitura/escrita em runtime — SQL direto, sem abstração de ORM.
* **EF Core Code-First** (`src/Infrastructure/Database/Context/AppDbContext.cs` + `Database/Configurations/**`) existe só para **versionar e aplicar o schema** (migrations) no Azure SQL centralizado. Não é usado para consultas da aplicação.

Essa separação — e por que o banco deixou de ser local por dev e passou a ser uma instância única do Azure SQL — está documentada em [ADR-0010](docs/adr/0010-adocao-ef-core-azure-sql.md).

### Infraestrutura (Azure)
* **Azure SQL Database:** instância única (`server-sql-vitalitas`), compartilhada por todo o time, schema versionado via EF Core migrations.
* **App Service:** hospedagem da API (deploy fora do escopo deste README).
* **Segredos locais:** `dotnet user-secrets`, nunca em arquivo versionado — ver [ADR-0011](docs/adr/0011-adocao-user-secrets.md) e a seção de configuração abaixo.

## Configuração do Ambiente de Desenvolvimento

Siga este guia para configurar o ambiente local, o banco de dados e as credenciais de segurança.

### Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas:
* **[.NET SDK 9.0+](https://dotnet.microsoft.com/download)**
* **[dotnet-ef](https://learn.microsoft.com/ef/core/cli/dotnet)** — não precisa instalar manualmente: é uma tool local do repositório (`.config/dotnet-tools.json`), restaurada com `dotnet tool restore` (ver abaixo).
* Acesso ao **Azure SQL Database** centralizado do projeto (peça as credenciais a quem administra o Azure do time — ver [ADR-0010](docs/adr/0010-adocao-ef-core-azure-sql.md)).

> O banco não é mais local por desenvolvedor. Desde o [ADR-0010](docs/adr/0010-adocao-ef-core-azure-sql.md), o Vitalitas usa uma **única instância do Azure SQL compartilhada por todo o time**, com o schema versionado via EF Core (Code-First/migrations) em vez de scripts `CREATE.sql`/`INSERT.sql` manuais.

### ⚠️ AVISO CRÍTICO DE WORKFLOW — banco compartilhado

Como **todo o time usa o mesmo banco no Azure SQL**, migrations de schema afetam todo mundo imediatamente:

* **Antes de subir (`git push`) uma migration nova**, avise o time (ex.: no canal do projeto) — outra pessoa pode estar com uma alteração de schema conflitante em andamento.
* **Depois de todo `git pull`**, rode `dotnet ef database update` (comando completo abaixo) para aplicar ao seu ambiente qualquer migration que outra pessoa tenha adicionado. Se pular esse passo, sua aplicação local pode quebrar ao rodar contra um schema desatualizado, ou você pode acidentalmente gerar uma migration duplicada/conflitante na próxima vez que alterar uma entidade.
* Nunca rode `dotnet ef database update` apontando para o Azure a partir de uma migration que você ainda não tem certeza que revisou — ela será aplicada para todo o time.

### Configuração Inicial (Devs)

#### 0. Liberar seu IP no firewall do Azure SQL

O Azure SQL só aceita conexões de IPs liberados. Antes de qualquer comando abaixo, peça a quem administra o Azure do time para:

1. Acessar o **Portal do Azure** → servidor SQL (`server-sql-vitalitas`) → **Segurança → Rede (Networking)** → aba **Regras de firewall**.
2. Adicionar uma regra com o nome do dev e o IP público dele (ex.: via [meuip.com.br](https://meuip.com.br)) — `Iniciar endereço IPv4` e `Endereço IPv4 final` iguais.
3. Salvar.

Sem essa liberação, `dotnet ef database update`, `dotnet run` e qualquer tentativa de conexão falham por timeout — não é um problema no seu código.

#### 1. Restaurar as ferramentas do repositório

```bash
dotnet tool restore
```

Isso instala a versão do `dotnet-ef` fixada em `.config/dotnet-tools.json`, usada para gerar e aplicar migrations.

#### 2. Configurar os segredos locais (`dotnet user-secrets`)

A connection string do Azure SQL e a chave JWT não vivem no código-fonte nem em `appsettings.Development.json` — desde o [ADR-0011](docs/adr/0011-adocao-user-secrets.md), usamos `dotnet user-secrets`, que guarda os valores fora da árvore do repositório.

Na pasta `src/API` (onde já existe um `UserSecretsId` configurado no `.csproj`):

```bash
cd src/API
dotnet user-secrets set "ConnectionStrings:ConexaoPadrao" "Server=tcp:SEU-SERVIDOR.database.windows.net,1433;Database=VITALITAS;User ID=SEU_USUARIO;Password=SUA_SENHA;Encrypt=True;TrustServerCertificate=False;"
dotnet user-secrets set "Jwt:Key" "SUA_CHAVE_SECRETA_LOCAL_COM_PELO_MENOS_32_CARACTERES"
```

`src/API/appsettings.Development.json.example` continua disponível só como referência do formato/chaves esperadas (não é mais o método usado para preencher os valores).

#### 3. Aplicar o schema ao Azure SQL

```bash
dotnet ef database update --project src/Infrastructure/Vitalitas.Infrastructure.csproj --startup-project src/API/Vitalitas.API.csproj
```

Isso aplica todas as migrations pendentes (incluindo `InitialCreate`, que cria as ~21 tabelas do domínio) no banco apontado pela sua connection string. Rode este comando sempre após um `git pull` que traga migrations novas (ver aviso acima).

Para gerar uma migration nova depois de alterar uma entidade em `src/Domain/Features/**`:

```bash
dotnet ef migrations add NomeDaMudanca --project src/Infrastructure/Vitalitas.Infrastructure.csproj --startup-project src/API/Vitalitas.API.csproj --output-dir Database/Migrations
```

### Executando a Aplicação
Com os segredos configurados e o schema aplicado, execute os comandos abaixo no terminal dentro da pasta do projeto:

```bash
dotnet restore
dotnet build
dotnet run --project src/API/Vitalitas.API.csproj
```

A API estará disponível em `https://localhost:7214` (HTTPS) ou `http://localhost:5156` (HTTP), conforme configurado em `launchSettings.json`.

### Troubleshooting

**`dotnet ef` falha com "You must install or update .NET to run this application" (framework `9.0.0` não encontrado):**
Sua máquina não tem o runtime .NET 9 instalado (só SDKs/runtimes de outras versões). Em vez de baixar o runtime, defina o roll-forward para a sessão do terminal e rode o comando de novo:

```powershell
$env:DOTNET_ROLL_FORWARD = "LatestMajor"
dotnet ef database update --project src/Infrastructure/Vitalitas.Infrastructure.csproj --startup-project src/API/Vitalitas.API.csproj
```

```bash
DOTNET_ROLL_FORWARD=LatestMajor dotnet ef database update --project src/Infrastructure/Vitalitas.Infrastructure.csproj --startup-project src/API/Vitalitas.API.csproj
```

Essa variável só vale para a sessão atual do terminal — precisa ser definida de novo se você abrir um terminal novo.

**Não faça `dotnet tool update dotnet-ef --version 8.x` (ou qualquer downgrade manual):**
O projeto usa EF Core **9.0.18** (ver `Directory.Packages.props`) e a tool local está pinada na mesma versão em `.config/dotnet-tools.json`. Rodar `dotnet tool update` apontando pra outra versão diverge do resto do projeto e gera o erro `NU1605` (conflito de versão) na próxima vez que alguém rodar `dotnet restore`. Se a tool sumir ou ficar desatualizada, o comando certo é sempre `dotnet tool restore` — ele lê a versão certa do manifesto.

**Erro de timeout/conexão recusada ao rodar `dotnet ef` ou `dotnet run`:**
Seu IP mudou (comum em rede residencial/4G) e caiu fora da regra de firewall do Azure SQL — peça pra atualizar a regra (ver "0. Liberar seu IP" acima). Também vale lembrar que o Azure SQL Serverless entra em modo de espera quando ocioso: a primeira conexão depois de um tempo parado pode levar de 20 a 45 segundos para responder (cold start) — não é travamento.

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
