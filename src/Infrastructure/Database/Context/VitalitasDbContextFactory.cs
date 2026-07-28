using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Database.Context
{
    /// <summary>
    /// Usado só em design-time pelo `dotnet ef` (migrations/database update), para não precisar
    /// subir a API inteira. Lê a mesma configuração (appsettings.json + env vars) que o Program.cs usa.
    /// </summary>
    public class VitalitasDbContextFactory : IDesignTimeDbContextFactory<VitalitasDbContext>
    {
        public VitalitasDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("../API/appsettings.json", optional: true)
                .AddJsonFile("../API/appsettings.Development.json", optional: true)
                .AddUserSecrets("vitalitas-backend-123")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("ConexaoPadrao")
                ?? throw new InvalidOperationException(
                    "Connection string 'ConexaoPadrao' não encontrada para o design-time factory do EF Core.");

            var optionsBuilder = new DbContextOptionsBuilder<VitalitasDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new VitalitasDbContext(optionsBuilder.Options);
        }
    }
}
