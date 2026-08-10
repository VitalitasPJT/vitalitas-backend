# Arquitetura do Backend — Vitalitas

> Este documento explica **como o backend do Vitalitas é organizado por dentro**, em linguagem simples. Não é preciso saber programar para entender — a ideia é que qualquer pessoa do time (dev, QA, gestor de produto, quem estiver começando agora) consiga ler e entender como as coisas se encaixam.
>
> Para o *porquê* técnico de cada decisão de organização, veja os ADRs em [`docs/adr/`](adr/). Este documento aqui é o "mapa de hoje"; os ADRs são o "diário de bordo" de como chegamos até ele.

---

## 1. A ideia por trás da organização

Pensa no backend como um **restaurante**:

- O **cliente** (o app / site que usamos no celular ou navegador) faz um pedido.
- Alguém **recebe o pedido** e leva até a cozinha (isso é a camada **API**).
- A **cozinha** sabe executar as receitas: como criar um novo aluno, como trocar uma senha, como registrar uma ficha médica (isso é a camada **Application**).
- Toda receita segue **regras da casa** — o que é obrigatório, o que não pode acontecer, o que é um "aluno" ou uma "academia" de verdade (isso é a camada **Domain**).
- E existe uma **despensa/estoque**, que sabe guardar e buscar os ingredientes — no nosso caso, o banco de dados (isso é a camada **Infrastructure**).

Cada uma dessas quatro partes só conhece a parte "de baixo" dela. A cozinha não precisa saber como o cliente fez o pedido (se foi pelo site, pelo app, por telefone); ela só recebe o pedido já traduzido. Isso é o que chamamos de **Clean Architecture**: cada parte tem uma responsabilidade clara, e é fácil trocar uma peça sem quebrar as outras.

```
Cliente (app / site)
    │
    ▼
API              → recebe o pedido, devolve a resposta
    │
    ▼
Application      → executa a "receita" (a regra do que fazer)
    │
    ▼
Domain           → as regras do negócio (o que é válido, o que não é)
    │
    ▼
Infrastructure   → conversa com o banco de dados
    │
    ▼
Banco de Dados
```

---

## 2. Organizado por assunto, não por camada

Além das 4 camadas, o código também é organizado por **assunto** (a gente chama de *feature*): tudo relacionado a "Membro" (o aluno da academia) fica junto, tudo relacionado a "Gestor" fica junto, e assim por diante — em **todas** as camadas, com a mesma pasta.

Isso significa que, se você quer entender ou mexer em "tudo que envolve o Membro", você sabe exatamente onde procurar em cada camada:

```
Membro (Member)

API              → Controllers/Users/MemberController.cs
Application      → Features/Users/Member/...
Domain           → Features/Users/Member/Entities/Member.cs
Infrastructure   → Repositories/Users/Member/MemberRepository.cs
```

Vantagem prática: qualquer pessoa nova no time acha rápido tudo que precisa sobre um assunto, sem precisar decorar a estrutura inteira do projeto.

---

## 3. Os nomes agora são em inglês

Até pouco tempo atrás, os nomes de pastas, arquivos e classes eram uma mistura de português e inglês (`Aluno`, `Gestor`, `Ficha` ao lado de `DTO`, `Request`, `UseCase`). Isso foi padronizado: **hoje tudo — pastas, arquivos, nomes de classes — está em inglês**, para facilitar caso o time cresça com pessoas de fora do Brasil e para seguir a convenção mais comum em projetos .NET.

Tabela de tradução dos termos de negócio, caso você esteja acostumado com os nomes antigos:

| Antes (PT) | Agora (EN) | O que é |
|---|---|---|
| Usuario | User | Qualquer pessoa com login no sistema |
| Aluno | Member | O aluno/membro da academia |
| Gestor | Manager | Quem administra a academia |
| Funcionario | Employee | Funcionário da academia |
| Instrutor | Instructor | Instrutor/personal |
| Administrador | Administrator | Acesso administrativo total |
| Academia | Gym | A academia em si |
| Ficha | TrainingSheet | Ficha de treino |
| FichaMedica | MedicalRecord | Ficha médica |
| Contrato | Contract | Contrato do membro |
| Licenca | License | Licença de uso do sistema pela academia |
| Plano | Plan | Plano/pacote (de contrato ou de licença) |
| Treino | Workout | Um treino |

Uma coisa importante: **só os nomes técnicos mudaram** (pasta, arquivo, nome de classe). O que já estava em português dentro do banco de dados como texto de negócio, ou em nomes de campo específicos, não foi mexido — só a "casca" que organiza o código.

> Lista completa de tudo que foi traduzido (entidades, enums, value objects, verbos de Request/Response e pastas) está em [`Glossario_Nomenclatura.md`](Glossario_Nomenclatura.md).

---

## 4. As quatro camadas, uma por uma

### 4.1 API — a porta de entrada

É quem recebe os pedidos (requisições HTTP) de quem usa o sistema, e devolve a resposta. **Não decide nada sozinha** — ela só recebe, repassa pra camada certa, e devolve o resultado.

```
API
│
├── Controllers        → um arquivo por "porta de entrada" (login, criar membro, etc.)
├── Services            → serviço de geração/validação do "crachá" de acesso (JWT)
├── Properties
├── Program.cs          → liga tudo isso quando o sistema sobe
├── appsettings.json               → configurações públicas (sem senhas)
└── appsettings.Development.json   → configurações locais de cada dev (não vai pro Git)
```

Portas de entrada que já existem hoje:

```
Controllers
│
├── Records
│     └── MedicalRecordController.cs      → ficha médica
│
└── Users
      ├── MemberController.cs             → membro/aluno
      ├── ManagerController.cs            → gestor
      └── UserController.cs               → login, troca de token, dados gerais de usuário
```

> Para o passo a passo de como configurar o ambiente local (`appsettings.Development.json`), veja o [README](../README.md).

### 4.2 Application — as "receitas"

Aqui vive **o que o sistema sabe fazer**: criar um membro, listar alunos de uma academia, trocar uma senha, gerar um novo token de acesso. Cada operação chama entidades do Domain e usa os repositórios (via contrato/interface) — mas **nunca fala direto com o banco de dados**.

```
Application
│
├── Features
│     ├── Users              → tudo sobre pessoas (Member, Manager, Employee, Instructor)
│     └── Records            → fichas (ficha de treino, ficha médica)
├── Token                    → login, renovação de token
├── Shared                   → coisas usadas por vários módulos (ex: resposta padrão de status)
└── Extensions                → liga os serviços dessa camada quando o sistema sobe
```

Cada assunto (*feature*) segue o mesmo padrão de pastas — mas só usa as pastas que realmente precisa. Exemplo de uma feature completa, `Member`:

```
Member
│
├── Constructor   → monta o objeto antes de mandar pro Domain
├── DTO           → formato de dado usado só pra transporte entre camadas
├── Request       → o que a operação recebe
├── Response      → o que a operação devolve
├── Interfaces    → o "contrato" de cada operação disponível
└── UseCases      → onde a regra realmente é executada
```

`Employee` e `Instructor`, por exemplo, hoje só têm a pasta `Constructor` — ainda não têm uma operação própria (Use Case): a criação deles hoje passa pelo `Manager`.

### 4.3 Domain — as regras do negócio

É o **coração** do sistema: o que é um Membro válido, o que é uma Academia, o que pode e o que não pode acontecer. Essa camada **não sabe nada sobre banco de dados, API ou qualquer tecnologia externa** — só sabe as regras do negócio. Isso é proposital: dá pra trocar o banco de dados inteiro, ou a forma como a API recebe pedidos, sem precisar tocar em uma linha sequer do Domain.

```
Domain
│
├── Features
│     ├── Users      → User, Member, Manager, Employee, Instructor, Administrator
│     ├── Gym        → a academia
│     ├── Plans       → Contract, License e seus planos
│     ├── Records     → TrainingSheet, MedicalRecord
│     ├── Token       → RefreshToken
│     └── Shared      → Schedule, ActivityLog, Workout
│
├── Enums            → listas fixas de opções (ex: tipo de usuário, status de contrato)
└── ValueObjects      → "pequenos objetos" que garantem que um dado está certo (CPF válido, e-mail com formato correto, etc.)
```

Exemplos de `Enums` hoje: `UserType`, `ContractStatus`, `LicenseType`, `Role`.
Exemplos de `ValueObjects` hoje: `CPF`, `CNPJ`, `Email`, `Name`, `CREF`, `Monetary` (valor em dinheiro).

### 4.4 Infrastructure — onde fica o banco de dados

Responsável por **conversar de verdade com o banco de dados**: guardar, buscar, atualizar e apagar informação. Implementa os contratos definidos no Domain — o Domain diz "eu preciso conseguir buscar um Membro pelo ID"; a Infrastructure é quem sabe como fazer isso de fato em SQL Server.

```
Infrastructure
│
├── Database
│     ├── Configurations   → como cada entidade vira tabela no banco
│     ├── Connections       → como o sistema se conecta ao banco
│     ├── Context           → configuração do Entity Framework (usado só pra manter o schema do banco em dia)
│     ├── Migrations        → histórico de mudanças no banco de dados
│     ├── Scripts           → scripts SQL manuais (criação, dados de teste, consultas)
│     └── Seed              → dados iniciais criados quando o sistema sobe
│
├── Repositories            → o acesso ao banco no dia a dia (usa Dapper, não Entity Framework)
├── Records                 → formato "cru" de alguns dados vindos do banco
└── Extensions               → liga os serviços dessa camada quando o sistema sobe
```

> **Detalhe técnico que vale saber:** o dia a dia do sistema (buscar, criar, atualizar dados) usa uma biblioteca chamada **Dapper**, que é mais direta e rápida. O **Entity Framework** só é usado para versionar e aplicar mudanças de estrutura no banco (as *migrations*) — não para consultas do dia a dia. Mais detalhes no [ADR-0010](adr/0010-adocao-ef-core-azure-sql.md).

Repositórios que já existem e funcionam hoje:

```
Repositories
│
├── Users
│     ├── Common    → UserRepository.cs
│     ├── Member    → MemberRepository.cs
│     └── Manager   → ManagerRepository.cs
│
├── Records
│     └── MedicalRecord → MedicalRecordRepository.cs
│
└── Token
      └── RefreshTokenRepository.cs
```

---

## 5. O que já funciona vs. o que está planejado

O modelo de dados (entidades, tabelas do banco) já existe pronto para bem mais coisa do que o sistema realmente faz hoje. Isso é normal em um projeto em construção: o "esqueleto" é feito primeiro, e a funcionalidade completa (tela, operação, regra) vem depois.

**Funcionando hoje, de ponta a ponta** (API → regra → banco):
- Login e renovação de sessão
- Cadastro e consulta de Membro, Gestor e demais usuários
- Ficha médica

**O modelo de dados já existe, mas a funcionalidade completa ainda não foi construída** (ou seja: a tabela existe no banco, mas ainda não tem uma tela/operação pronta pra usar):
- Academia (Gym)
- Contrato e Licença, e os planos de cada um (Contract, License, LicensePlan, ContractPlan)
- Agenda (Schedule)
- Histórico de atividade (ActivityLog)
- Frequência do aluno (Attendance)
- Histórico de XP (XpHistory)

Se você for implementar uma dessas próximas, siga o mesmo padrão descrito na seção 4 — o modelo de dados já está pronto, falta só a camada de Application (Use Case) e o Controller na API.

---

## 6. Autenticação

O fluxo completo de login, token e permissões tem um documento próprio: [`Autenticacao_API.md`](Autenticacao_API.md).

---

## 7. Para saber o "porquê" de cada decisão

Cada mudança estrutural relevante feita neste projeto tem um ADR (*Architecture Decision Record*) próprio em [`docs/adr/`](adr/), explicando o contexto, o que foi decidido, o que mais foi considerado e as consequências. Vale a leitura se você quiser entender a história por trás de uma convenção, não só a regra em si.

---

## 8. Por que organizar assim

- Cada camada tem uma responsabilidade clara — fica mais fácil saber onde procurar (e onde mexer) quando algo precisa mudar.
- Baixo risco de um ajuste em um lugar quebrar algo em outro lugar sem relação.
- Mais fácil de testar cada parte separadamente.
- Fácil de crescer: uma feature nova segue sempre o mesmo padrão de pastas.
- Qualquer pessoa nova no time consegue se localizar rápido, sem precisar perguntar "onde fica isso?" toda hora.
