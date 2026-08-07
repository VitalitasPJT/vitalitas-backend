using Domain.Features.Usuarios.Common.Interfaces;
using Domain.Features.Usuarios.Aluno.Interfaces;
using Domain.Features.Usuarios.Gestor.Interfaces;
using Domain.Features.Fichas.FichaMedica.Interfaces;
using Domain.Features.Token.Interfaces;
using Infrastructure.Repositories.Usuarios.Common;
using Infrastructure.Repositories.Usuarios.Aluno;
using Infrastructure.Repositories.Usuarios.Gestor;
using Infrastructure.Repositories.Fichas.FichaMedica;
using Infrastructure.Repositories.Token;
using Infrastructure.Database.Connections;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DbConnectionFactory>();

            // AppDbContext é usado apenas para versionar/aplicar o schema (migrations) no
            // Azure SQL centralizado — o acesso a dados em runtime continua via Dapper
            // (ver DbConnectionFactory acima e Infrastructure/Repositories). Ver ADR-0010.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConexaoPadrao")
                        ?? throw new InvalidOperationException("String de conexão 'ConexaoPadrao' não encontrada.")));

            services.AddUsuarioFeature();
            services.AddAlunoFeature();
            services.AddGestorFeature();
            services.AddFichaMedicaFeature();
            services.AddTokenFeature();

            return services;
        }

        private static IServiceCollection AddUsuarioFeature(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            return services;
        }

        private static IServiceCollection AddAlunoFeature(this IServiceCollection services)
        {
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            return services;
        }

        private static IServiceCollection AddGestorFeature(this IServiceCollection services)
        {
            services.AddScoped<IGestorRepository, GestorRepository>();
            return services;
        }

        private static IServiceCollection AddFichaMedicaFeature(this IServiceCollection services)
        {
            services.AddScoped<IFichaMedicaRepository, FichaMedicaRepository>();
            return services;
        }

        private static IServiceCollection AddTokenFeature(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
