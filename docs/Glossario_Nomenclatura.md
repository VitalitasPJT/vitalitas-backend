# Glossário de Nomenclatura — PT → EN

> Este documento existe para quem já conhecia o backend com os nomes antigos (em português) e precisa de um "tradutor" rápido para encontrar as coisas nos nomes novos (em inglês).
>
> A tradução foi feita na refatoração descrita no [ADR-0003](adr/0003-padronizar-nomenclatura-de-pastas.md) e no [ADR-0004](adr/0004-corrigir-nomes-de-arquivo-tipo-enganosos.md), usando rename por símbolo (Roslyn) — ou seja, só o nome mudou, o comportamento do sistema continua o mesmo.
>
> Veja também a visão geral da organização do backend em [`Arquitetura_backend.md`](Arquitetura_backend.md).

---

## 1. Conceitos de negócio (entidades)

| Antes (PT) | Agora (EN) | O que é |
|---|---|---|
| Usuario | User | Qualquer pessoa com login no sistema |
| Aluno | Member | O aluno/membro da academia |
| Gestor | Manager | Quem administra a academia |
| Funcionario | Employee | Funcionário da academia |
| Instrutor | Instructor | Instrutor/personal |
| Administrador | Administrator | Acesso administrativo total ao sistema |
| Academia | Gym | A academia em si |
| TelefoneAcademia / TelefoneUsuario | GymPhone / UserPhone | Telefone associado à academia ou ao usuário |
| Ficha | TrainingSheet | Ficha de treino |
| FichaMedica | MedicalRecord | Ficha médica do aluno |
| Exercicio | Exercise | Um exercício dentro de uma ficha de treino |
| Contrato | Contract | Contrato do membro com a academia |
| Licenca | License | Licença de uso do sistema pela academia |
| Plano / Planos | Plan / Plans | Plano/pacote (de contrato ou de licença) |
| PlanoContrato | ContractPlan | Plano associado a um contrato |
| PlanoLicenca | LicensePlan | Plano associado a uma licença |
| Treino / Treinos | Workout | Um treino |
| Agenda | Schedule | Agendamento/horário |
| Frequencia | Attendance | Frequência/presença do aluno na academia |
| XpHistorico | XpHistory | Histórico de pontos de experiência (gamificação) |
| LogAtividade | ActivityLog | Registro de atividade no sistema |
| Compartilhado | Shared | Pasta com itens usados por vários módulos |

---

## 2. Enums

| Antes (PT) | Agora (EN) | O que é |
|---|---|---|
| StatusContrato | ContractStatus | Situação atual de um contrato |
| StatusLicenca | LicenseStatus | Situação atual de uma licença |
| TipoLicenca | LicenseType | Tipo/categoria de licença |
| TipoAcademia | GymType | Tipo/categoria de academia |
| TipoUsuario | UserType | Tipo de usuário (aluno, gestor, etc.) |
| TipoTreino | WorkoutType | Tipo/categoria de treino |
| StatusAgenda | ScheduleStatus | Situação de um agendamento |
| AcaoLog | LogAction | Ação registrada em um log de atividade |
| Cargo | Role | Cargo/papel de um usuário no sistema |

---

## 3. Value Objects

| Antes (PT) | Agora (EN) | O que é |
|---|---|---|
| Monetario | Monetary | Valor em dinheiro, com suas regras de validação |
| Nome | Name | Nome de pessoa, com suas regras de validação |

---

## 4. Verbos em Request/Response

Os verbos usados nos nomes de classes de `Request` e `Response` também foram traduzidos:

| Antes (PT) | Agora (EN) |
|---|---|
| Criar | Create |
| Listar | List |
| Obter | Get |
| Atualizar | Update |

Exemplos de classes que mudaram de nome (dentro de `Application/Features/Users/Manager/Response/`):

| Antes (PT) | Agora (EN) |
|---|---|
| CriarAlunoResponse | CreateMemberResponse |
| CriarFuncionarioResponse | CreateEmployeeResponse |
| CriarGestorResponse | CreateManagerResponse |
| CriarInstrutorResponse | CreateInstructorResponse |
| CriarUsuarioResponse | CreateUserResponse |
| ListarAlunosResponse | ListMembersResponse |
| ListarUsuarioResponse | ListUserResponse |
| ListarUsuariosResponse | ListUsersResponse |
| ObterGestorResponse | GetManagerResponse |

---

## 5. Pastas e *features*

| Antes (PT) | Agora (EN) |
|---|---|
| Usuarios | Users |
| Aluno | Member |
| Gestor | Manager |
| Funcionario | Employee |
| Instrutor | Instructor |
| Administrador | Administrator |
| Academia | Gym |
| Fichas | Records |
| FichaMedica | MedicalRecord |
| Ficha | TrainingSheet |
| Planos | Plans |
| Contrato | Contract |
| Licenca | License |
| Compartilhado | Shared |

---

## 6. O que não mudou

Só a "casca" que organiza o código (pasta, arquivo, nome de classe) foi traduzida. O que já estava em português como **texto de negócio dentro do banco de dados**, ou nomes de campo específicos que não faziam parte dessa lista, não foi alterado.
