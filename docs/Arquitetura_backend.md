# Arquitetura Backend - Vitalitas

## Visão Geral

O backend do Vitalitas foi organizado utilizando os princípios da **Clean Architecture**, com separação em camadas independentes. Cada camada possui uma responsabilidade específica, reduzindo o acoplamento e facilitando manutenção, testes e evolução da aplicação.

A estrutura foi organizada por **domínios (features)**, permitindo que funcionalidades relacionadas permaneçam agrupadas em todas as camadas do sistema.

```
Backend
│
├── API
├── Application
├── Domain
└── Infrastructure
```

Cada camada possui uma responsabilidade bem definida.

---

# Estrutura Geral

```
Backend
│
├── API
├── Application
├── Domain
└── Infrastructure
```

## Fluxo da aplicação

```
Cliente
    │
    ▼
Controllers (API)
    │
    ▼
Use Cases (Application)
    │
    ▼
Domain
    │
    ▼
Repositories (Infrastructure)
    │
    ▼
Banco de Dados
```

---

# API

A camada **API** representa o ponto de entrada da aplicação.

Sua responsabilidade é receber requisições HTTP, validar informações básicas, encaminhar a execução para a camada Application e devolver a resposta ao cliente.

Nenhuma regra de negócio deve ser implementada nesta camada.

## Estrutura

```
API
│
├── Controllers
├── Services
├── Properties
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

---

## Controllers

Os Controllers recebem todas as requisições da aplicação.

Cada Controller representa um recurso da API.

Exemplos:

```
Controllers

AlunoController

GestorController

UsuarioController

FichaMedicaController

AgendaController
```

Responsabilidades:

- Receber requisições HTTP
- Validar parâmetros básicos
- Chamar o Use Case correspondente
- Retornar respostas HTTP

Não devem conter regras de negócio.

---

## Services

Contém serviços utilizados apenas pela API.

Exemplo:

```
JwtService
```

Responsável por:

- geração de JWT
- validação de token
- autenticação

Caso existam outros serviços exclusivos da API, eles permanecem nesta pasta.

---

## Program.cs

Responsável por inicializar toda a aplicação.

Realiza:

- configuração da API
- Dependency Injection
- configuração do JWT
- Swagger
- CORS
- Middlewares

---

## appsettings.json

Arquivos de configuração da aplicação.

Exemplos:

- Connection Strings
- JWT
- Configurações gerais

---

# Application

A camada Application representa os **casos de uso** do sistema.

Toda funcionalidade disponível ao usuário é implementada nesta camada.

Ela orquestra entidades do Domain e utiliza interfaces de repositórios.

Não possui acesso direto ao banco de dados.

---

## Organização

A arquitetura foi organizada por funcionalidades.

```
Application
│
├── Usuarios
├── Fichas
├── Token
├── Calculations
└── Shared
```

Cada módulo contém seus próprios arquivos.

---

# Estrutura de uma Feature

Cada funcionalidade segue exatamente o mesmo padrão.

Exemplo:

```
Usuarios
│
└── Aluno
      │
      ├── Constructor
      ├── DTOs
      ├── Requests
      ├── Responses
      ├── Interfaces
      └── UseCases
```

Essa padronização facilita a manutenção e torna toda feature previsível.

---

## Constructor

Responsável pela criação dos objetos utilizados pela camada Application.

Centraliza a construção de objetos complexos antes de enviá-los ao Domain.

Exemplo:

```
ConstructorAluno.cs
```

---

## DTOs

Representam objetos utilizados apenas para transferência de dados entre camadas.

Os DTOs evitam expor diretamente as entidades do domínio.

Exemplo:

```
AlunoDTO.cs

UsuarioDTO.cs
```

---

## Requests

Representam os dados recebidos pelas operações.

Exemplo:

```
CriarAlunoRQ.cs

AtualizarAlunoRQ.cs
```

Cada Request representa uma operação da aplicação.

---

## Responses

Representam os dados retornados pelos casos de uso.

Exemplo:

```
AlunoRP.cs

FichaMedicaRP.cs
```

Além dos dados retornados, normalmente incluem informações de status da operação.

---

## Interfaces

Define os contratos dos Use Cases.

Exemplo:

```
IAlunoUseCase.cs
```

A API conhece apenas essas interfaces.

---

## UseCases

Representam a implementação das regras da aplicação.

Exemplo:

```
AlunoUC.cs
```

Responsabilidades:

- executar regras de negócio
- utilizar entidades do Domain
- acessar repositórios através das interfaces
- montar respostas

Idealmente, cada operação pode evoluir para um Use Case próprio.

Exemplo:

```
CriarAlunoUseCase

AtualizarAlunoUseCase

BuscarAlunoUseCase

TrocarSenhaUseCase
```

---

# Calculations

Centraliza cálculos utilizados por diferentes funcionalidades.

Exemplo:

```
CalculoIMC

CalculoFC
```

Sempre que um cálculo puder ser reutilizado por vários módulos, ele deve permanecer nesta pasta.

---

# Token

Agrupa toda lógica relacionada à autenticação.

```
Token

Interfaces

Settings

Services

UseCases
```

Responsabilidades:

- Refresh Token
- autenticação
- geração de token
- validação

---

# Shared

Contém componentes compartilhados entre diversos módulos da Application.

Exemplo:

```
StatusHTTP.cs
```

Arquivos presentes nesta pasta não pertencem exclusivamente a nenhuma feature.

---

# Domain

Representa o núcleo da aplicação.

Toda regra de negócio pertence ao Domain.

O Domain não conhece:

- banco de dados
- API
- Entity Framework
- SQL
- Controllers

É completamente independente.

---

## Organização

```
Domain
│
├── Academia
├── Usuarios
├── Planos
├── Fichas
├── Token
├── Shared
├── Enums
└── ValueObjects
```

Cada módulo representa um domínio da aplicação.

---

# Estrutura de um módulo

Exemplo:

```
Usuarios

Aluno

Entities

Interfaces
```

---

## Entities

Representam os objetos do negócio.

Exemplo:

```
Aluno

Usuario

Gestor

Funcionario

Instrutor
```

As entidades possuem comportamento e regras próprias.

---

## Interfaces

Representam contratos para acesso aos dados.

Exemplo:

```
IAlunoRepository

IGestorRepository
```

A camada Domain conhece apenas contratos.

A implementação ocorre na Infrastructure.

---

# Shared

Contém entidades compartilhadas entre diversos módulos.

Exemplo:

```
LogAtividade

Agenda
```

Caso uma entidade passe a possuir muitas responsabilidades, poderá evoluir para um módulo próprio.

---

# Enums

Centraliza todos os enumeradores utilizados pelo sistema.

Exemplo:

```
TipoUsuario

StatusContrato

TipoLicenca

Cargo
```

---

# ValueObjects

Representam objetos imutáveis utilizados pelo domínio.

Exemplos:

```
CPF

CNPJ

Email

Nome

CREF

Monetario
```

Possuem validações próprias e garantem consistência dos dados.

---

# Infrastructure

Responsável por toda comunicação com recursos externos.

Exemplos:

- Banco de Dados
- SQL
- Conexões
- Persistência

A Infrastructure implementa os contratos definidos pelo Domain.

---

## Organização

```
Infrastructure
│
├── Database
├── Repositories
├── Configurations
├── ExternalServices
├── Security
├── Logging
└── Extensions
```

Conforme o projeto evolui, novas pastas podem ser adicionadas sem alterar a estrutura existente.

---

# Repositories

Implementam as interfaces definidas pelo Domain.

Exemplo:

```
Repositories

Usuarios

AlunoRepository

UsuarioRepository

GestorRepository
```

Responsabilidades:

- executar consultas
- inserir dados
- atualizar registros
- excluir registros

Toda comunicação com o banco ocorre nesta camada.

---

# Database

Centraliza toda configuração relacionada ao banco de dados.

```
Database
│
├── Connections
├── Models
└── Scripts
```

---

## Connections

Responsável pela criação das conexões com o banco.

Exemplo:

```
DbConnectionFactory.cs
```

---

## Models

Representam modelos utilizados para persistência.

Exemplo:

```
UsuarioRecord.cs

RefreshTokenRecord.cs
```

Esses modelos representam a estrutura do banco.

---

## Scripts

Contém scripts SQL utilizados durante desenvolvimento.

Organização recomendada:

```
Scripts

Schema

Seed

Queries
```

### Schema

Scripts de criação do banco.

Exemplo:

```
CreateTables.sql

Indexes.sql

Views.sql
```

---

### Seed

Scripts de inserção inicial.

Exemplo:

```
InsertUsuarios.sql

InsertAcademias.sql
```

---

### Queries

Consultas SQL reutilizadas.

Exemplo:

```
BuscarAluno.sql

BuscarUsuarios.sql
```

---

# Configurations

Centraliza configurações específicas da infraestrutura.

Exemplos:

- configuração de banco
- configuração de JWT
- Dependency Injection

---

# ExternalServices

Serviços externos utilizados pela aplicação.

Exemplos futuros:

```
EmailService

StorageService

PagamentoService

NotificationService
```

---

# Security

Componentes relacionados à segurança.

Exemplos:

```
PasswordHasher

JwtGenerator

PermissionService
```

---

# Logging

Responsável pelo registro de eventos da aplicação.

Exemplo:

- logs
- auditoria
- monitoramento

---

# Extensions

Métodos de extensão utilizados pela infraestrutura.

Exemplo:

- configuração de serviços
- extensões para Dependency Injection

---

# Padrão de Organização

Toda nova funcionalidade deve seguir o mesmo padrão em todas as camadas.

Exemplo:

```
Aluno

API
│
└── AlunoController

Application
│
└── Usuarios
      └── Aluno

Domain
│
└── Usuarios
      └── Aluno

Infrastructure
│
└── Repositories
      └── Usuarios
            └── AlunoRepository
```

Dessa forma, qualquer desenvolvedor consegue localizar rapidamente todos os arquivos relacionados a uma funcionalidade específica.

---

# Benefícios da Arquitetura

- Separação clara de responsabilidades.
- Organização por domínio (feature-based).
- Baixo acoplamento entre camadas.
- Facilidade de manutenção.
- Facilidade para criação de testes.
- Escalabilidade para novas funcionalidades.
- Reutilização de código.
- Padronização da estrutura do projeto.
- Facilidade de navegação para novos desenvolvedores.
- Alinhamento com os princípios da Clean Architecture e boas práticas de desenvolvimento em .NET.