# Vitalitas - Backend API

> **Sistema Web Integrado de Gestão para Academias**

![Status](https://img.shields.io/badge/Status-Em_Desenvolvimento-yellow)
![Contexto](https://img.shields.io/badge/Contexto-Acadêmico-blue)
![.NET](https://img.shields.io/badge/.NET-Core-purple)
![SQL Server](https://img.shields.io/badge/Database-SQL_Server-red)

---

## Contexto Acadêmico

Este projeto é desenvolvido como parte da disciplina de **Projeto Integrador** do curso de Ciência da Computação/Sistemas de Informação do **Centro Universitário de Brasília (UniCEUB)**.

O desenvolvimento ocorre em um ambiente controlado, com supervisão docente, visando aplicar boas práticas de Engenharia de Software na solução de um problema real de mercado.

## Proposta de Valor

O **Vitalitas** não é apenas um sistema de cadastro; é uma solução estratégica para modernizar a gestão de academias.

### O Problema
Atualmente, a gestão de alunos, treinos e avaliações físicas em muitas academias é realizada através de processos manuais ou ferramentas fragmentadas. Isso gera **retrabalho**, **inconsistência de dados** e dificuldade no acompanhamento da evolução dos alunos.

### A Solução e Benefícios
O Vitalitas centraliza todas as informações operacionais e técnicas em uma única plataforma web. A entrega de valor do projeto se baseia em:

* **Eficiência Operacional:** Automatização de tarefas administrativas e controle de acesso.
* **Confiabilidade:** Dados centralizados e seguros para apoio à tomada de decisão.
* **Foco no Aluno:** Melhor acompanhamento de treinos e avaliações físicas, permitindo uma experiência mais personalizada.
* **Escalabilidade:** Arquitetura preparada para crescimento e futuras integrações mobile.

## O Projeto (MVP)

O escopo atual do projeto foca no **Produto Mínimo Viável (MVP)**. O objetivo desta etapa é entregar um conjunto mínimo de funcionalidades que permita a operação básica da academia, gerando valor imediato ao cliente e permitindo a validação técnica da solução em ambiente real.

### Funcionalidades Entregues

O MVP foi estruturado para cobrir os fluxos críticos da academia:

#### 1. Autenticação e Segurança
Garante que apenas usuários autorizados acessem o sistema, protegendo os dados sensíveis dos alunos.
* **Login Seguro:** Autenticação via e-mail e senha com criptografia.
* **Controle de Acesso (RBAC):** Identificação e redirecionamento automático por perfil (Administrador, Professor, Aluno).
* **Primeiro Acesso:** Fluxo obrigatório de alteração de senha para novos usuários.

#### 2. Gestão Administrativa (Backoffice)
Centraliza o cadastro de pessoas, eliminando fichas de papel e planilhas desconexas.
* **CRUD de Usuários:** Criação, edição e desativação de Administradores, Funcionários, Professores e Alunos.
* **Regras de Negócio:** Implementação de hierarquia onde funcionários não podem criar outros funcionários ou administradores.
* **Dados de Saúde:** Criação e monitoramento obrigatório da ficha médica do aluno no ato do cadastro.

#### 3. Gestão Técnica e Treinos
O coração da operação da academia, focado na experiência do professor e do aluno.
* **Fichas de Treino:** Criação e edição completa de rotinas de exercícios.
* **Histórico de Evolução:** Funcionalidade para comparar a ficha atual com a anterior.
* **Agendamento:** Sistema de agenda para marcar avaliações físicas.

#### 4. Portais Específicos
Interfaces dedicadas para cada tipo de usuário.
* **Visão do Professor:** Acesso aos alunos vinculados e ferramentas de edição de dados.
* **Visão do Aluno:** Visualização da ficha de treino ativa, agenda de avaliações e dados cadastrais.

### Fora do Escopo (MVP)
Para garantir a entrega dentro do prazo acadêmico e focar na qualidade das funcionalidades principais, os seguintes itens serão desenvolvidos em etapas futuras (V2):
* Gamificação e Ranking por XP.
* Integração financeira e pagamentos online.
* Funcionalidades avançadas de Inteligência Artificial.
* Cadastro complexo de bioimpedância.

## Arquitetura e Design

> 🚧 **EM REVISÃO ACADÊMICA:** Os diagramas e decisões arquiteturais abaixo estão em fase de validação pelos orientadores do projeto e podem sofrer alterações.

O projeto foi concebido seguindo os princípios de desenvolvimento de software moderno, priorizando a escalabilidade, a segurança e a manutenibilidade do código.

### Padrão Arquitetural
O backend do Vitalitas é desenvolvido como uma **API RESTful**, operando de forma *stateless* (sem estado). Isso significa que cada requisição HTTP contém todas as informações necessárias para ser processada, facilitando a escalabilidade horizontal na nuvem.

A estrutura do projeto segue a separação de responsabilidades (ex: `src/Infrastructure`), garantindo que as regras de negócio não estejam acopladas aos detalhes de banco de dados ou interface.

* **API:** Pontos de entrada (Endpoints) e validação de requisições.
* **Application/Domain:** Regras de negócio e entidades principais.
* **Infrastructure:** Acesso a dados, configurações de banco e integrações externas.

### Modelagem de Dados
O sistema utiliza um **Banco de Dados Relacional (SQL Server)** para garantir a integridade das informações críticas, como dados de saúde e histórico de treinos.

As principais entidades mapeadas incluem:
* **Usuários e Perfis:** Controle hierárquico (Admin, Professor, Aluno).
* **Treinos e Fichas:** Estrutura de exercícios e rotinas de treino.
* **Avaliações Físicas:** Histórico de medidas e composição corporal.

### Infraestrutura e Deploy
O ambiente de produção e homologação é hospedado no **Microsoft Azure**, utilizando:
* **Azure App Service:** Para hospedagem da API.
* **Azure SQL Database:** Para armazenamento dos dados relacionais.

## Configuração do Ambiente de Desenvolvimento

Siga este guia para configurar o ambiente local, o banco de dados e as credenciais de segurança.

### Pré-requisitos
Certifique-se de ter as seguintes ferramentas instaladas:
* **[.NET SDK 8.0+](https://dotnet.microsoft.com/download)**
* **[SQL Server 2022 Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)** 
* **[SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)** 

### 1. Configuração do Banco de Dados
1.  **Conexão Inicial:** Abra o SSMS e conecte-se à sua instância local (ex: `(localdb)\MSSQLLocalDB` ou `.\SQLEXPRESS`).
2.  **Configuração de Usuário:**
    *Crie um novo Login com **Autenticação SQL** (não use apenas a do Windows).
    * Desmarque a opção *"Impor política de senha"* para facilitar o desenvolvimento.
    * Defina o banco de dados padrão como `master` e garanta que a função de servidor `public` esteja marcada.
3.  **Criação do Banco:**
    * Crie um novo banco de dados com o nome: `[VITALITAS_DEV]`.
    * Defina o "Proprietário" (Owner) como o usuário que você acabou de criar.
4.  **Carga de Dados:**
    * No SSMS, abra e execute o script de criação:
        `vitalitas-backend\src\Infrastructure\Database\CREATE.sql`
    * Em seguida, execute o script de população de dados:
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

## Equipe do Projeto

Este projeto foi desenvolvido pelos alunos:

* **Sanderson Machado** - *Gerente de Projeto & Desenvolvedor Backend*
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN_AQUI)
    * [![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](LINK_DO_GITHUB_AQUI)

* **Hugo Matos** - *Desenvolvedor*
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN_AQUI)
    * [![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](LINK_DO_GITHUB_AQUI)

* **Pedro Luis de Souza Abreu** - *Desenvolvedor*
    * [![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](LINK_DO_LINKEDIN_AQUI)
    * [![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](LINK_DO_GITHUB_AQUI)

## Licença

Este projeto foi desenvolvido para fins acadêmicos na disciplina de **Projeto Integrador** do **UniCEUB**.

Copyright © 2026 **Vitalitas**. Todos os direitos reservados.