using Application.Features.Users.Common.Interfaces;
using Application.Features.Users.Common.UseCases;
using Application.Features.Users.Member.Interfaces;
using Application.Features.Users.Member.UseCases;
using Application.Features.Users.Manager.Interfaces;
using Application.Features.Users.Manager.UseCases;
using Application.Features.Records.MedicalRecord.Interfaces;
using Application.Features.Records.MedicalRecord.UseCases;
using Application.Token.Interfaces;
using Application.Token.UseCases;
using Application.Token.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUsuarioFeature();
            services.AddAlunoFeature();
            services.AddGestorFeature();
            services.AddFichaMedicaFeature();
            services.AddTokenFeature(configuration);

            return services;
        }

        private static IServiceCollection AddUsuarioFeature(this IServiceCollection services)
        {
            services.AddScoped<IUserUseCase, UserUC>();
            return services;
        }

        private static IServiceCollection AddAlunoFeature(this IServiceCollection services)
        {
            services.AddScoped<IMemberUseCase, MemberUC>();
            return services;
        }

        private static IServiceCollection AddGestorFeature(this IServiceCollection services)
        {
            services.AddScoped<IManagerUseCase, ManagerUC>();
            return services;
        }

        private static IServiceCollection AddFichaMedicaFeature(this IServiceCollection services)
        {
            services.AddScoped<IMedicalRecordUseCase, MedicalRecordUC>();
            return services;
        }

        private static IServiceCollection AddTokenFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRefreshTokenUseCase, RefreshTokenUC>();
            services.Configure<RefreshTokenSettings>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}
