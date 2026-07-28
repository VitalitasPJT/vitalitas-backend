# Scripts SQL legados (arquivados)

`CREATE.sql`, `INSERT.sql` e `SELECT.sql` eram rodados manualmente no SSMS/`sqlcmd` para criar o schema e popular dados de teste, antes da migração para Entity Framework Core (ver [`docs/adr/0014`](../../adr/0014-migracao-dapper-para-ef-core.md)).

Nenhum código ou processo de deploy os lê mais. O schema atual é criado por `dotnet ef database update` (migrations em `src/Infrastructure/Database/Migrations/`), e os dados iniciais de desenvolvimento vêm do `DevelopmentSeeder` (`src/Infrastructure/Database/Seed/`).

Mantidos aqui só como referência histórica do schema anterior à migração.
