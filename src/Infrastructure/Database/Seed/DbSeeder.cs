using Domain.Features.Plans.Contract.Entities;
using Domain.Features.Plans.License.Entities;
using Domain.ValueObjects;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed
{
    /// <summary>
    /// Seed idempotente: cada bloco só grava se a tabela correspondente já não tiver
    /// dados, então pode ser chamado repetidamente (toda inicialização, todo ambiente)
    /// sem duplicar registros no Azure SQL compartilhado.
    ///
    /// Hoje só popula o catálogo de planos (dado de referência, sem informação sensível).
    /// Seeds de dados de negócio (ex.: usuário administrador inicial) não foram incluídos
    /// aqui de propósito — exigem uma senha/segredo real, que não deve ficar hardcoded
    /// no código-fonte. Se o time decidir ter um admin inicial, gere a senha via
    /// user-secrets/variável de ambiente e adicione um bloco seguindo o mesmo padrão
    /// (verificação `!context.Set&lt;T&gt;().Any()` antes de inserir).
    /// </summary>
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken = default)
        {
            await SeedPlanosLicencaAsync(context, cancellationToken);
            await SeedPlanosContratoAsync(context, cancellationToken);
        }

        private static async Task SeedPlanosLicencaAsync(AppDbContext context, CancellationToken cancellationToken)
        {
            if (await context.PlanosLicenca.AnyAsync(cancellationToken))
            {
                return;
            }

            context.PlanosLicenca.AddRange(
                new LicensePlan("Básica", "Licença básica para uma única academia.", new Monetary("199.90")),
                new LicensePlan("Premium", "Licença premium com suporte prioritário e múltiplas unidades.", new Monetary("499.90"))
            );

            await context.SaveChangesAsync(cancellationToken);
        }

        private static async Task SeedPlanosContratoAsync(AppDbContext context, CancellationToken cancellationToken)
        {
            if (await context.PlanosContrato.AnyAsync(cancellationToken))
            {
                return;
            }

            context.PlanosContrato.Add(
                new ContractPlan("Mensal", "Plano de contrato padrão, renovação mensal.", new Monetary("99.90")));

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
