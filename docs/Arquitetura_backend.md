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

## Convenções de nomenclatura

Estas convenções foram fixadas na revisão de arquitetura e devem ser seguidas em qualquer feature nova:

- **Namespace = caminho da pasta.** O namespace de um arquivo reflete exatamente sua localização física (ex: `src/Domain/Features/Usuarios/Aluno/Entities/Aluno.cs` → `namespace Domain.Features.Usuarios.Aluno.Entities`). Se mover o arquivo de pasta, o namespace muda junto.
- **PascalCase em todas as pastas** (`Usuarios`, `Aluno`, `Entities`, `Interfaces`), inclusive quando o nome de dentro é um termo técnico (`Token`, `Compartilhado`).
- **Português para nomes de domínio, inglês para termos técnicos genéricos.** Entidades e features de negócio ficam em português (`Usuarios`, `Academia`, `Planos`, `Fichas`, `Gestor`, `Aluno`...); termos sem tradução natural no jargão do time ficam em inglês (`Shared`, `Token`, `DTO`, `UseCase`, `Constructor`).
- **Singular nas pastas de Request/Response** (`Request`, `Response`, não `Requests`/`Responses`).
- **Nome do arquivo = nome do tipo principal.** Um arquivo `IAlunoRepository.cs` deve declarar `interface IAlunoRepository`, não um nome abreviado como `IAluno`.
- **Evite nomear uma entidade igual ao segmento de namespace da própria feature** (ex: uma classe `Academia` dentro do namespace `Domain.Features.Academia`) — o C# resolve esse conflito tratando o nome como namespace em vez de tipo, e força qualificação completa (`Domain.Features.Academia.Entities.Academia`) em quem consome a classe.

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

Cada Controller representa um recurso da API, agrupado em subpastas por feature (mesmo padrão da Application e do Domain).

Estrutura atual:

```
Controllers
│
├── Agenda
│     └── AgendaDBController.cs
│
├── Fichas
│     ├── AvaliacaoDBController.cs
│     ├── FichaMedicaController.cs
│     └── FichasDBController.cs
│
└── Usuarios
      ├── AlunoController.cs
      ├── GestorController.cs
      └── UsuarioController.cs
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
└── Compartilhado
```

Cada módulo contém seus próprios arquivos.

---

# Estrutura de uma Feature

Cada funcionalidade segue o mesmo padrão de pastas. Nem toda feature usa todas as pastas — só existem as que a feature realmente precisa (ex: `Funcionario` e `Instrutor` hoje só têm `Constructor`, porque ainda não têm Use Case próprio).

Exemplo (feature completa, `Aluno`):

```
Usuarios
│
└── Aluno
      │
      ├── Constructor
      ├── DTO
      ├── Request
      ├── Response
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

ConstructorGestor.cs

ConstructorFuncionario.cs

ConstructorInstrutor.cs

ConstructorFichaMedica.cs
```

> **Pendência conhecida:** `Application/Compartilhado/UserModels.cs` contém uma cópia duplicada de todas as classes `Constructor*` acima, dentro de uma classe `Constructor` "guarda-chuva". Hoje é essa cópia duplicada que está realmente em uso pelo código (via `using static Application.Compartilhado.Constructor;`), não os arquivos individuais listados aqui. Resolver essa duplicação é um item aberto da revisão de arquitetura — a decisão já tomada é manter os arquivos por feature (como listado acima) e eliminar `UserModels.cs`.

---

## DTO

Representam objetos utilizados apenas para transferência de dados entre camadas.

Os DTOs evitam expor diretamente as entidades do domínio.

Exemplo:

```
AlunoDTO.cs

UsuarioDTO.cs

GestorDTO.cs
```

---

## Request

Representam os dados recebidos pelas operações. Pasta e namespace no singular (`Request`, não `Requests`).

Exemplo:

```
AlunoRQ.cs

GestorRQ.cs

UsuarioRQ.cs

FichaMedicaRQ.cs
```

Cada arquivo `*RQ.cs` agrupa os registros de request de uma feature; cada operação tem seu próprio tipo dentro do arquivo (ex: `TrocarSenhaRequest`, `VincularInstrutorRequest`).

---

## Response

Representam os dados retornados pelos casos de uso. Pasta e namespace no singular (`Response`, não `Responses`).

Exemplo:

```
AlunoRP.cs

UsuarioRP.cs

FichaMedicaRP.cs
```

Além dos dados retornados, normalmente incluem informações de status da operação (`StatusHTTP`).

Padrão recomendado para features novas: **uma classe por arquivo**, sem agrupar tudo dentro de uma classe "wrapper". É o que a feature `Gestor` já segue hoje — `CriarAlunoResponse.cs`, `CriarInstrutorResponse.cs`, `ListarAlunosResponse.cs`, etc. Os arquivos `AlunoRP.cs`/`UsuarioRP.cs` ainda usam o padrão antigo (classe wrapper com responses aninhadas) e devem migrar para esse formato quando forem alterados.

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
CalculosF.cs   (classe CalculosFeminino)

CalculosM.cs   (classe CalculosMasculino)
```

Sempre que um cálculo puder ser reutilizado por vários módulos, ele deve permanecer nesta pasta.

---

# Token

Agrupa toda lógica relacionada à autenticação.

```
Token

Interfaces

Settings

Service

UseCases
```

Responsabilidades:

- Refresh Token
- autenticação
- geração de token
- validação

---

# Compartilhado

Contém componentes compartilhados entre diversos módulos da Application.

Exemplo:

```
StatusHTTP.cs
```

Arquivos presentes nesta pasta não pertencem exclusivamente a nenhuma feature.

> `UserModels.cs` também está nesta pasta hoje, mas é a duplicação pendente descrita na seção Constructor — não deve ser usado como referência de padrão.

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
├── Features
│     ├── Academia
│     ├── Usuarios
│     ├── Planos
│     ├── Fichas
│     ├── Token
│     └── Shared
│
├── Enums
└── ValueObjects
```

`Enums` e `ValueObjects` ficam fora de `Features` porque são transversais — usados por várias features ao mesmo tempo, não pertencem a uma feature específica. Cada pasta dentro de `Features` representa um domínio da aplicação.

---

# Estrutura de um módulo

Exemplo:

```
Features

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

IUsuarioRepository

IFichaMedicaRepository
```

O nome do arquivo e o nome da interface são sempre idênticos (`IAlunoRepository.cs` declara `interface IAlunoRepository`, nunca um nome abreviado como `IAluno`).

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
├── Database        (implementado)
└── Repositories     (implementado)
```

As pastas abaixo ainda **não existem** no projeto — são a organização planejada para quando a Infrastructure crescer, não uma descrição do estado atual:

```
├── Configurations   (planejado)
├── ExternalServices (planejado)
├── Security         (planejado)
├── Logging          (planejado)
└── Extensions       (planejado)
```

Conforme o projeto evolui, essas pastas podem ser adicionadas sem alterar a estrutura existente.

---

# Repositories

Implementam as interfaces definidas pelo Domain.

**Estado atual (pendência conhecida):** ao contrário de `Domain/Features` e `Application`, os repositórios ainda estão numa pasta única, sem subpastas por feature:

```
Repositories

AlunoRepository.cs
FichaMedicaRepository.cs
GestorRepository.cs
RefreshTokenRepository.cs
UsuarioRepository.cs
```

Organização planejada (ainda não feita), espelhando `Domain/Features`:

```
Repositories
│
└── Usuarios
      └── Aluno
            └── AlunoRepository.cs
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
Infrastructure
│
├── Database
│     ├── Connections
│     └── Scripts
│
└── Records
```

`Records` é uma pasta própria, direto sob `Infrastructure` (não fica dentro de `Database`).

---

## Connections

Responsável pela criação das conexões com o banco.

Exemplo:

```
DbConnectionFactory.cs
```

---

## Records

Representam modelos utilizados para persistência (mapeiam o resultado das queries antes de virar entidade de Domain).

Exemplo atual:

```
UsuarioDB.cs

RefreshTokenDB.cs
```

Esses modelos representam a estrutura do banco.

---

## Scripts

Contém scripts SQL utilizados durante desenvolvimento. Hoje fica em `Database/Scripts`, ainda em formato flat (sem subpastas):

```
CREATE.sql

INSERT.sql

SELECT.sql
```

Organização por Schema/Seed/Queries é uma evolução futura, não o estado atual.

---

# Configurations (planejado)

> Esta pasta ainda não existe no projeto. Descrição do que ela deve conter quando for criada.

Centraliza configurações específicas da infraestrutura.

Exemplos:

- configuração de banco
- configuração de JWT
- Dependency Injection

---

# ExternalServices (planejado)

> Esta pasta ainda não existe no projeto. Descrição do que ela deve conter quando for criada.

Serviços externos utilizados pela aplicação.

Exemplos futuros:

```
EmailService

StorageService

PagamentoService

NotificationService
```

---

# Security (planejado)

> Esta pasta ainda não existe no projeto. Hoje a geração/validação de JWT vive em `API/Services/JwtService.cs`. Descrição do que `Security` deve conter quando for criada na Infrastructure.

Componentes relacionados à segurança.

Exemplos:

```
PasswordHasher

JwtGenerator

PermissionService
```

---

# Logging (planejado)

> Esta pasta ainda não existe no projeto. Descrição do que ela deve conter quando for criada.

Responsável pelo registro de eventos da aplicação.

Exemplo:

- logs
- auditoria
- monitoramento

---

# Extensions (planejado)

> Esta pasta ainda não existe no projeto. Hoje todo o `builder.Services.AddScoped<...>()` fica direto em `API/Program.cs`, sem nenhuma separação por camada/feature — é um item pendente da revisão de arquitetura mover isso para métodos de extensão (`AddApplicationServices()`, `AddInfrastructureServices()`) nesta pasta.

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
└── Controllers
      └── Usuarios
            └── AlunoController.cs

Application
│
└── Usuarios
      └── Aluno
            └── UseCases
                  └── AlunoUC.cs

Domain
│
└── Features
      └── Usuarios
            └── Aluno
                  └── Entities
                        └── Aluno.cs

Infrastructure
│
└── Repositories
      └── AlunoRepository.cs   (pendente: mover para Repositories/Usuarios/Aluno/)
```

Dessa forma, qualquer desenvolvedor consegue localizar rapidamente todos os arquivos relacionados a uma funcionalidade específica. A Infrastructure é a única camada que ainda não segue esse padrão por completo — ver pendência na seção Repositories.

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