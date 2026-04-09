# Vitalitas - Backend API

> **API RESTful para gestão integrada de academias.**

![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-yellow?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Azure](https://img.shields.io/badge/Azure-0089D6?style=for-the-badge&logo=microsoft-azure&logoColor=white)

> ℹ️ **Visão Geral:** Para entender o contexto acadêmico, a proposta de valor e o escopo do produto (MVP), acesse o **[README da Organização Vitalitas](https://github.com/VitalitasPJT)**.

## Arquitetura e Design

> 🚧 **EM REVISÃO ACADÊMICA:** Os diagramas e decisões arquiteturais abaixo estão em fase de validação pelos orientadores do projeto e podem sofrer alterações.

O backend foi desenvolvido seguindo princípios de **Clean Architecture** e **Domain-Driven Design (DDD)** simplificado, visando desacoplamento entre as regras de negócio e a infraestrutura.

### Visão da Solução
A API atua como o núcleo central do sistema, operando de forma *stateless* e servindo os clientes web/mobile.

![Diagrama de Arquitetura](./arquitetura.png)
*(Fluxo: React Client ↔ API .NET Core ↔ SQL Server / Azure Services)*

### Modelagem de Dados
A estrutura relacional foi projetada no **SQL Server** para garantir a integridade de dados críticos como fichas médicas e histórico de treinos.

![Diagrama Entidade Relacionamento](./der_database.png)
*(Principais entidades: Usuários, Perfis, Treinos, Fichas e Avaliações)*

### Infraestrutura
O projeto utiliza a nuvem da **Microsoft Azure**:
* **App Service:** Hospedagem da API.
* **Azure SQL Database:** Persistência dos dados.

## Configuração do Ambiente de Desenvolvimento

Siga este guia para configurar o ambiente local, o banco de dados e as credenciais de segurança.

### Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas:
* **[.NET SDK 8.0+](https://dotnet.microsoft.com/download)**
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

A API estará disponível em https://localhost:7167 (ou porta similar).

## Documentação da API

A API segue o padrão RESTful e sua documentação interativa é gerada automaticamente via **Swagger/OpenAPI**.

### Acesso ao Swagger
Após iniciar a aplicação localmente, a documentação completa dos endpoints, esquemas de requisição e tipos de resposta estará disponível em:

> **https://localhost:7167/swagger**
> *(A porta pode variar dependendo da sua configuração local no `launchSettings.json`)*

### Autenticação e Segurança
O sistema utiliza **JSON Web Tokens (JWT)** para segurança. A maioria dos endpoints é protegida e requer um token válido.

#### Como se Autenticar
1.  Utilize o endpoint `POST /vitalitas/user/login` para enviar suas credenciais (email e senha).
2.  A API retornará um **Token Bearer**.
3.  Inclua este token no cabeçalho (*Header*) de todas as requisições subsequentes:

```http
Authorization: Bearer <seu_token_aqui>
```

## Equipe Backend

Responsáveis pela arquitetura, banco de dados e regras de negócio:

* **Sanderson Machado** - *Gerente de Projeto / Tech Lead*
    * **Foco:** Arquitetura Backend, Definição de Backlog (PO) e Liderança Técnica.
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](LINK_DO_GITHUB)

* **Hugo Matos** - *DBA / QA*
    * **Foco:** Modelagem de dados (DER/MER), Scripts SQL e Testes de Qualidade.
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](LINK_DO_GITHUB)

* **Pedro Luis de Souza Abreu** - *Desenvolvedor Back-end*
    * **Foco:** Desenvolvimento de APIs, Regras de Negócio e Integração com Banco.
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=flat-square&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN) [![GitHub](https://img.shields.io/badge/GitHub-100000?style=flat-square&logo=github&logoColor=white)](LINK_DO_GITHUB)

## Licença

Este projeto foi desenvolvido exclusivamente para fins acadêmicos na disciplina de **Projeto Integrador** do **Centro Universitário de Brasília (UniCEUB)**.

Copyright © 2026 **Vitalitas**. Todos os direitos reservados.
