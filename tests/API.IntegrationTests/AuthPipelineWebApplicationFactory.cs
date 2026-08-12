using System.Collections.Generic;
using API.IntegrationTests.Fakes;
using Domain.Features.Token.Interfaces;
using Domain.Features.Users.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.IntegrationTests
{
    // Sobe o pipeline HTTP real (UseAuthentication/UseAuthorization/[Authorize]/
    // policies nomeadas) em memória via TestServer. Só os repositórios que falam
    // com o Azure SQL via Dapper (IUserRepository, IRefreshTokenRepository) são
    // trocados por fakes — o resto do pipeline de auth roda sem nenhum mock.
    public class AuthPipelineWebApplicationFactory : WebApplicationFactory<Program>
    {
        public const string JwtKey = "chave-de-teste-somente-para-integration-tests-nao-usar-em-producao";
        public const string JwtIssuer = "vitalitas-tests";
        public const string JwtAudience = "vitalitas-tests";

        public FakeUserRepository Users { get; } = new();
        public FakeRefreshTokenRepository RefreshTokens { get; } = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // "Testing" (não "Development") pra pular o bloco de Swagger/seed do
            // AppDbContext em Program.cs — esses testes nunca tocam banco real.
            builder.UseEnvironment("Testing");

            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Key"] = JwtKey,
                    ["Jwt:Issuer"] = JwtIssuer,
                    ["Jwt:Audience"] = JwtAudience,
                    ["Jwt:DurationInMinutes"] = "15",
                    // Nunca é aberta de verdade (Manager/MedicalRecord repos não são
                    // exercitados por nenhum caminho de sucesso nestes testes) — só
                    // precisa existir pra DbConnectionFactory não explodir no
                    // construtor ao ser injetado em controllers não relacionados.
                    ["ConnectionStrings:ConexaoPadrao"] = "Server=(local);Database=nao-usado-nestes-testes;Trusted_Connection=True;TrustServerCertificate=True;"
                });
            });

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IUserRepository>();
                services.AddSingleton<IUserRepository>(Users);

                services.RemoveAll<IRefreshTokenRepository>();
                services.AddSingleton<IRefreshTokenRepository>(RefreshTokens);
            });
        }
    }
}
