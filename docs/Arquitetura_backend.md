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

### Multi-tenancy

Cada Academia é um tenant (ver ADR 0013). O tenant é identificado exclusivamente pela claim `TenantId` do JWT — nunca por header, subdomínio ou campo do corpo da requisição — e resolvido cedo no pipeline pelo `TenantResolutionMiddleware`. Entidades tenant-scoped carregam `IdAcademia` denormalizado para viabilizar isolamento automático de dados (`HasQueryFilter`, ainda pendente de implementação — ver ADR 0013, seção Pendências).

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
├── Fichas
│     └── FichaMedicaController.cs
│
└── Usuarios
      ├── AlunoController.cs
      ├── GestorController.cs
      └── UsuarioController.cs
```

`AgendaDBController`, `AvaliacaoDBController` e `FichasDBController` (treino) existiam antes, mas eram inteiramente código comentado de uma versão anterior com Entity Framework, sem nenhum endpoint funcionando — foram removidos na limpeza de código morto. As entidades `Agenda`, `Avaliacao` e `Treino` já têm mapeamento EF Core (`DbSet<T>` + Configuration, ver seção Database), mas nenhum repositório/Use Case/Controller foi implementado para elas ainda — se virarem features reais, a implementação deve seguir o padrão EF Core usado no resto do projeto (ver seção Repositories).

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

`appsettings.json` fica versionado e não deve conter segredos — `ConnectionStrings:ConexaoPadrao` e `Jwt:Key` ficam vazios nele de propósito. `appsettings.Development.json` é onde cada dev preenche os próprios valores locais; esse arquivo está no `.gitignore` e não é versionado.

Para configurar o ambiente local pela primeira vez, copie o exemplo:

```bash
cp src/API/appsettings.Development.json.example src/API/appsettings.Development.json
```

E preencha `ConnectionStrings:ConexaoPadrao` com a sua connection string local e `Jwt:Key` com uma chave secreta de pelo menos 32 caracteres (qualquer string aleatória serve em ambiente de desenvolvimento).

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
├── Database     (implementado)
├── Repositories (implementado)
└── Extensions   (implementado — ver seção Extensions)
```

As pastas abaixo ainda **não existem** no projeto — são a organização planejada para quando a Infrastructure crescer, não uma descrição do estado atual:

```
├── Configurations   (planejado)
├── ExternalServices (planejado)
├── Security         (planejado)
└── Logging          (planejado)
```

Conforme o projeto evolui, essas pastas podem ser adicionadas sem alterar a estrutura existente.

---

# Repositories

Implementam as interfaces definidas pelo Domain, organizados em subpastas por feature — mesmo padrão de `Domain/Features` e `Application`:

```
Repositories
│
├── Usuarios
│     ├── Aluno
│     │     └── AlunoRepository.cs
│     ├── Common
│     │     └── UsuarioRepository.cs
│     └── Gestor
│           └── GestorRepository.cs
│
├── Fichas
│     └── FichaMedica
│           └── FichaMedicaRepository.cs
│
└── Token
      └── RefreshTokenRepository.cs
```

`UsuarioRepository` fica em `Usuarios/Common` porque implementa `IUsuarioRepository`, que vive em `Domain/Features/Usuarios/Common/Interfaces` — o subnível espelha exatamente onde a interface correspondente está no Domain, não o nome da classe.

Responsabilidades:

- executar consultas
- inserir dados
- atualizar registros
- excluir registros

Toda comunicação com o banco ocorre nesta camada. Desde a migração para EF Core (ADR 0014), cada repositório recebe `VitalitasDbContext` via injeção de dependência (em vez de `DbConnectionFactory`) e usa `DbSet<T>`/LINQ no lugar de SQL cru.

---

# Database

Centraliza toda configuração relacionada ao banco de dados. Desde a sessão que introduziu Entity Framework Core (ver ADR 0014), o acesso a dados é feito via `DbContext` e Migrations Code-First — não há mais SQL manual nem `DbConnectionFactory`.

```
Infrastructure
└── Database
      ├── Context         (VitalitasDbContext + factory de design-time)
      ├── Configurations   (Fluent API, IEntityTypeConfiguration<T> — uma classe por entidade)
      ├── Converters       (ValueConverter<T,string> para cada Value Object do Domain)
      ├── Migrations       (histórico de schema gerado por `dotnet ef migrations add`)
      └── Seed             (DevelopmentSeeder — dados mínimos para logar em Development)
```

Os scripts SQL legados (`CREATE.sql`/`INSERT.sql`/`SELECT.sql`) foram arquivados fora da árvore de código, em `docs/archive/sql-scripts-legado/` — ver seção "Scripts (arquivados)" abaixo.

---

## Context

`VitalitasDbContext` expõe um `DbSet<T>` por entidade de Domain e aplica todas as `Configurations` via `ApplyConfigurationsFromAssembly` no `OnModelCreating` — não há Fluent API escrita diretamente no `DbContext`, cada entidade tem sua própria classe de configuração.

`VitalitasDbContextFactory` implementa `IDesignTimeDbContextFactory<VitalitasDbContext>` e é usado só pela CLI `dotnet ef` (migrations, database update) — lê a mesma cadeia de configuração (`appsettings*.json` + User Secrets + variáveis de ambiente) que o host real usa, sem precisar subir a API inteira.

---

## Configurations

Uma classe `IEntityTypeConfiguration<T>` por entidade, organizada em subpastas espelhando `Domain/Features/**` (mesma convenção de namespace-por-pasta do resto do projeto). Define chaves, tamanhos de coluna, índices únicos e a conversão de cada Value Object via `HasConversion<XConverter>()`.

Nenhuma relação `HasOne`/`WithMany` é configurada — o Domain não tem propriedades de navegação entre entidades, só chaves estrangeiras escalares (`Guid`). Joins entre entidades relacionadas são feitos explicitamente em LINQ nos repositórios, quando necessário.

> Nota: esta pasta não deve ser confusa com o item "Configurations (planejado)" que aparecia aqui em revisões anteriores deste documento — aquele item genérico (configuração de banco/JWT/DI) nunca chegou a ser criado como pasta própria; a config tipada de cada seção já vive nos `Settings`/`Extensions` de cada camada (ver ADR 0010).

---

## Converters

Um `ValueConverter<TModel, TProvider>` por Value Object do Domain que precisa ser persistido (`Nome`, `Email`, `CPF`, `CREF`, `CNPJ`, `Monetario`). A entidade continua expondo o VO fortemente tipado; a conversão para o tipo primitivo do banco (geralmente `string`) acontece só na fronteira do EF Core.

Atenção ao usar esses VOs em queries LINQ: o EF Core traduz a comparação da propriedade inteira (`u.Email == new Email(x)`) para SQL, mas **não** traduz acesso a um membro do VO dentro da expressão (`u.Email.Valor == x`) — isso lança `InvalidOperationException` em runtime, não erro de compilação. Onde é preciso projetar `.Valor` de um VO, a entidade completa é materializada primeiro (`.ToList()`/`.FirstOrDefault()`), e o `.Valor` é lido depois, já em memória.

---

## Migrations

Histórico de schema versionado, gerado por `dotnet ef migrations add <Nome>` e aplicado por `dotnet ef database update` (ou automaticamente em Development, via `dbContext.Database.Migrate()` no `Program.cs`). Substitui rodar `CREATE.sql` manualmente no SSMS.

---

## Seed

`DevelopmentSeeder` substitui `INSERT.sql`. Roda condicionalmente (só se a tabela `Academias` estiver vazia) depois do `Migrate()`, em Development, criando o mínimo necessário para logar: 1 Academia + 1 Usuário Gestor. O restante dos dados de teste é criado através da própria API (endpoints de `GestorController`), não de um script de seed extenso.

---

## Scripts (arquivados)

`CREATE.sql`, `INSERT.sql` e `SELECT.sql` ficaram órfãos com a migração para EF Core — movidos para `docs/archive/sql-scripts-legado/` como referência histórica do schema anterior, não são mais executados por nenhum processo. A pasta `Database/Scripts` deixou de existir em `Infrastructure`.

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

# Extensions

Métodos de extensão de `IServiceCollection` para registrar Dependency Injection. Diferente do que a versão original deste documento previa, `Extensions` não é exclusiva da Infrastructure — **cada camada registra a si mesma**, então existe uma pasta `Extensions` em `Application`, em `Infrastructure` e em `API`:

```
Application/Extensions/ApplicationServiceExtensions.cs
Infrastructure/Extensions/InfrastructureServiceExtensions.cs
API/Extensions/ApiServiceExtensions.cs
```

Cada `Add*Services()` público é dividido internamente em métodos privados por feature (`AddAlunoFeature()`, `AddGestorFeature()`, `AddTokenFeature()`, etc.), mantendo a mesma granularidade usada no resto do projeto. `Program.cs` fica reduzido a três chamadas:

```csharp
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApiServices();
```

`AddApiServices()` existe porque `JwtService` é uma classe da própria API (implementa tanto `API.Services.IJwtService` quanto `Application.Token.Service.ITokenService`) — como `Application` não referencia `API` (a dependência só existe no sentido contrário), esse registro não pode morar em `AddApplicationServices()`; fica na própria API.

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
      └── Usuarios
            └── Aluno
                  └── AlunoRepository.cs
```

Dessa forma, qualquer desenvolvedor consegue localizar rapidamente todos os arquivos relacionados a uma funcionalidade específica. As quatro camadas seguem esse padrão hoje.

---

# Registro de Decisões (ADR)

Cada mudança estrutural feita na revisão de arquitetura tem um ADR próprio em [`docs/adr/`](adr/), documentando o contexto, a decisão tomada, as alternativas consideradas e as consequências. Consulte esses arquivos para entender o *porquê* por trás de cada convenção listada acima, não só o *o quê*.

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