using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database.Context
{
    /// <summary>
    /// Permite que o `dotnet ef` construa o AppDbContext sem inicializar o host completo
    /// da API (que valida configuração de JWT na inicialização). Lê a mesma connection
    /// string ("ConexaoPadrao") configurada via user-secrets no projeto API — ver ADR-0011.
    /// Use sempre com --startup-project apontando para src/API.
    /// </summary>
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private const string ApiUserSecretsId = "vitalitas-backend-123";

        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddUserSecrets(ApiUserSecretsId)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("ConexaoPadrao")
                ?? throw new InvalidOperationException(
                    "String de conexão 'ConexaoPadrao' não encontrada. Configure-a via " +
                    "'dotnet user-secrets set \"ConnectionStrings:ConexaoPadrao\" \"...\"' no projeto src/API " +
                    "antes de rodar comandos do EF Core (ver README).");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
