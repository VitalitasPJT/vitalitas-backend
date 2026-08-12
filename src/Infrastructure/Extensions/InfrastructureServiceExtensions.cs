using Domain.Features.Users.Common.Interfaces;
using Domain.Features.Users.Member.Interfaces;
using Domain.Features.Users.Manager.Interfaces;
using Domain.Features.Records.MedicalRecord.Interfaces;
using Domain.Features.Shared.Interfaces;
using Domain.Features.Token.Interfaces;
using Infrastructure.Repositories.Users.Common;
using Infrastructure.Repositories.Users.Member;
using Infrastructure.Repositories.Users.Manager;
using Infrastructure.Repositories.Records.MedicalRecord;
using Infrastructure.Repositories.ActivityLog;
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
            services.AddActivityLogFeature();
            services.AddTokenFeature();

            return services;
        }

        private static IServiceCollection AddUsuarioFeature(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        private static IServiceCollection AddAlunoFeature(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            return services;
        }

        private static IServiceCollection AddGestorFeature(this IServiceCollection services)
        {
            services.AddScoped<IManagerRepository, ManagerRepository>();
            return services;
        }

        private static IServiceCollection AddFichaMedicaFeature(this IServiceCollection services)
        {
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            return services;
        }

        private static IServiceCollection AddActivityLogFeature(this IServiceCollection services)
        {
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            return services;
        }

        private static IServiceCollection AddTokenFeature(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
