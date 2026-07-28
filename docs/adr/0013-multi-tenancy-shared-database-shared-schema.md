# 0013 — Multi-Tenancy: Shared Database, Shared Schema

## Status

**Backlog removido do board em 2026-07-28** — o usuário decidiu que a iniciativa de Multi-Tenancy não condiz com o que o projeto precisa agora (não fazia parte da concepção original do Scrum do projeto). As Fases 3–5 (`HasQueryFilter`, `TenantSaveChangesInterceptor`, correção do `GestorController.CriarUsuario`, testes de isolamento, onboarding de tenant) **não estão mais planejadas** — não é "adiado para a próxima sessão", é removido do escopo.

O que já foi implementado (Fases 0–2 + parte da Fase 5: claim `TenantId` no JWT, `ITenantContext`, `TenantResolutionMiddleware`, `IdAcademia` denormalizado em 16 entidades, índices compostos, EF Core) **permanece no código por decisão explícita do usuário** — não atrapalha o desenvolvimento atual (a claim é só mais um dado no token; o middleware só valida que ela existe) e não vale o retrabalho de reverter agora. Tratar como capacidade dormente, não como trabalho em andamento.

## Contexto

O backlog de Multi-Tenant (HU-01 a HU-04) pede que cada Academia opere como um tenant isolado dentro do mesmo sistema: usuários, alunos, fichas, treinos etc. de uma Academia nunca devem vazar para outra, mesmo que o Id de um registro de outro tenant seja adivinhado ou forjado.

Havia três decisões de modelagem a tomar antes de qualquer código:

1. **Quem é o tenant?** O domínio já tem o conceito de `Academia` (nome, CNPJ, endereço, licença). Criar uma entidade `Tenant` separada duplicaria esse conceito sem necessidade.
2. **De onde vem o tenant em cada requisição?** Um cliente HTTP poderia, em teoria, mandar o tenant via header (`X-Tenant-Id`), subdomínio, ou campo no corpo da requisição. Todas essas opções dependem do cliente não mentir.
3. **Qual estratégia de isolamento de dados?** As opções clássicas são banco por tenant, schema por tenant, ou schema compartilhado com uma coluna discriminadora.

## Decisão

- **Tenant = `Academia`.** Nenhuma entidade nova. O `IdAcademia` já existente é o identificador de tenant em todo o sistema.
- **Origem do tenant = exclusivamente a claim `"TenantId"` do JWT**, nunca header, subdomínio ou campo do payload da requisição. Isso é decidido na origem (login), não checado depois — o que torna HU-03 ("tenant nunca vem de input do cliente que pode ser forjado") verdadeiro por construção, em vez de precisar de auditoria posterior em cada endpoint.
  - `JwtService.GenerateToken` agora recebe `tenantId` e grava `new Claim("TenantId", tenantId)`.
  - `ITenantContext` (`src/Domain/Features/Shared/Interfaces/ITenantContext.cs`) expõe `Guid TenantId { get; }`; a implementação concreta `CurrentTenantService` (API, por depender de `IHttpContextAccessor`) lê essa claim do usuário autenticado.
  - `TenantResolutionMiddleware` roda logo após `UseAuthentication()` e recusa (401) qualquer requisição autenticada sem uma claim `TenantId` válida — falha cedo, antes de chegar em qualquer Use Case.
- **Shared Database, Shared Schema**: uma única base de dados, um único schema, com `IdAcademia` como coluna discriminadora em toda tabela tenant-scoped. Descartadas as alternativas de banco-por-tenant e schema-por-tenant (ver "Alternativas consideradas").
- **`IdAcademia` denormalizado diretamente** em toda entidade tenant-scoped, mesmo nas que hoje só chegam lá via 1–2 saltos de FK (`Aluno`, `Instrutor`, `Funcionario`, `Administrador`, `Gestor`, `RefreshToken`, `LogAtividade`, `XpHistorico`, `Frequencia`, `FichaMedica`, `TelefoneUsuario`, `Avaliacao`, `Ficha`, `Treino`, `Agenda`). Isso é o que permite um filtro global (`HasQueryFilter`) funcionar sem exigir `JOIN` em toda query — pré-requisito técnico para a Fase 3.
  - `Contrato`, `PlanoContrato`, `Licenca`, `PlanoLicenca` ficam **fora** do escopo de tenant: são catálogo/assinatura cross-tenant, não pertencem a uma única Academia.

## Alternativas consideradas

- **Banco de dados por tenant** — isolamento mais forte, mas custo operacional alto (uma conexão/migração por Academia) e não condizente com o volume esperado (academias de porte pequeno/médio). Descartado.
- **Schema por tenant** — meio-termo, mas EF Core não tem suporte de primeira classe para "schema dinâmico por requisição" sem trabalho manual equivalente ao do filtro global. Não trouxe benefício que justificasse a complexidade adicional frente ao Shared Schema.
- **Tenant via header HTTP (`X-Tenant-Id`)** — descartado porque exigiria confiar no cliente para não mentir sobre qual Academia ele representa; qualquer bug de validação vira vazamento de dados entre tenants. A claim do JWT já é assinada pelo servidor no login, então não pode ser forjada sem a chave secreta.
- **Entidade `Tenant` nova, separada de `Academia`** — rejeitada por duplicar um conceito que já existe no domínio sem necessidade real (nenhum caso de uso pede uma Academia pertencer a múltiplos tenants, ou um tenant sem Academia).

## Consequências

- O isolamento de dados (HU-02) só passa a ser real quando o `HasQueryFilter` global entrar em vigor (Fase 3) — até lá, o `IdAcademia` está disponível em todo lugar necessário, mas nenhuma query é filtrada automaticamente. Ver Pendências.
- Toda nova feature que crie uma entidade tenant-scoped precisa lembrar de incluir `IdAcademia` desde o início — não há enforcement de compilador para isso (é convenção, documentada aqui e em `docs/Arquitetura_backend.md`).
- O `TenantResolutionMiddleware` cobre only requisições autenticadas; endpoints `[AllowAnonymous]` (login, refresh) não passam por ele, propositalmente — o tenant ainda não existe antes do login.

## Removido do escopo em 2026-07-28 (não é mais pendência, é decisão de não fazer)

- Fase 3: `IHasTenant` (marcador) + `HasQueryFilter` genérico aplicado a todo tipo `IHasTenant` no `OnModelCreating` + `TenantSaveChangesInterceptor` (stampa `IdAcademia` automaticamente ao inserir, usando `ITenantContext`).
- Fase 4: auditoria de `IgnoreQueryFilters` e correção de `GestorController.CriarUsuario`/`CriarUsuarioRequest.IdAcademia`, que recebe o tenant direto do corpo da requisição sem validar contra `ITenantContext.TenantId` do Gestor autenticado — um Gestor da Academia A pode hoje criar um usuário informando o `IdAcademia` da Academia B. **Continua existindo no código** (não foi corrigido), mas deixou de ser tratado como risco de "vazamento entre tenants" já que a iniciativa de Multi-Tenancy foi removida — vale mais como um lembrete de validação de entrada genérica caso o campo `IdAcademia` no payload volte a importar por outro motivo.
- Fase 5: onboarding de nova Academia, testes automatizados de isolamento.

Já feito nesta sessão (2026-07-28) antes da remoção do backlog, adiantando parte da Fase 5 — fica no código como já explicado no Status:
- Índices compostos `(IdAcademia, PK)` em todas as 16 entidades tenant-scoped (`Usuarios`, `Alunos`, `Instrutores`, `Funcionarios`, `Gestores`, `Administradores`, `RefreshTokens`, `LogsAtividade`, `XpHistoricos`, `Frequencias`, `FichasMedicas`, `TelefonesUsuario`, `Fichas`, `Treinos`, `Avaliacoes`, `Agendas`) — verificado via `sys.indexes` que todos os 16 índices foram criados.
- `.github/workflows/ci.yml`: roda `dotnet build` + `dotnet test` em push/PR para `prd`/`des`. É um gate de build/teste genérico — ainda não é um "gate de isolamento" de verdade, porque não existe teste de isolamento nenhum até a Fase 3 acontecer. Quando os testes de vazamento entre tenants forem escritos, já vão rodar automaticamente neste workflow, sem precisar de mudança nele.

## Avaliação de limites de escala

Estimativa qualitativa, baseada na estratégia Shared Database/Shared Schema com os índices compostos já em vigor (não é um teste de carga real — é uma expectativa de engenharia a validar quando houver tráfego real):

- **Número de tenants (Academias)**: não há limite técnico rígido — `IdAcademia` é só uma coluna discriminadora, não um schema/banco por tenant. Centenas de Academias não deveriam gerar nenhum problema perceptível.
- **Volume por tenant**: com os índices compostos `(IdAcademia, PK)`, uma query filtrada por tenant faz um seek direto em vez de scan, então o crescimento do volume total do sistema não deveria degradar a latência de queries de um tenant individual — até a faixa de dezenas de milhões de linhas por tabela, que é onde um único SQL Server geralmente começa a exigir revisão de estratégia independente de multi-tenancy.
- **Onde a estratégia atual provavelmente vai precisar de revisão**:
  - **"Noisy neighbor"**: como todos os tenants compartilham a mesma instância/banco, um tenant com carga de escrita muito acima da média pode degradar performance para os demais — não há isolamento de recursos (CPU/IO) entre tenants nesta estratégia.
  - **Backup/restore granular**: não é possível restaurar os dados de uma única Academia isoladamente sem tooling adicional (teria que ser via export/import lógico, não backup nativo do SQL Server).
  - **Antes disso tudo**: o ponto mais urgente de revisão não é escala, é o `HasQueryFilter` (Fase 3) ainda não existir — sem ele, todo o argumento de isolamento por índice é sobre performance, não sobre segurança.
- **Sinal para reavaliar**: se o número de tenants crescer para a casa dos milhares, ou se uma única Academia se tornar desproporcionalmente grande (ex.: uma rede/franquia grande demais dividindo a mesma instância com academias pequenas), aí sim vale reconsiderar particionamento por `IdAcademia` ou schema-per-tenant para os tenants grandes — não antes disso, seria otimização prematura.
