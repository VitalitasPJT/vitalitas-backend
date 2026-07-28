using Application.Usuarios.Common.Interfaces;
using Application.Usuarios.Common.UseCases;
using Application.Usuarios.Aluno.Interfaces;
using Application.Usuarios.Aluno.UseCases;
using Application.Usuarios.Gestor.Interfaces;
using Application.Usuarios.Gestor.UseCases;
using Application.Fichas.FichaMedica.Interfaces;
using Application.Fichas.FichaMedica.UseCases;
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
            services.AddScoped<IUsuarioUseCase, UsuarioUC>();
            return services;
        }

        private static IServiceCollection AddAlunoFeature(this IServiceCollection services)
        {
            services.AddScoped<IAlunoUseCase, AlunoUC>();
            return services;
        }

        private static IServiceCollection AddGestorFeature(this IServiceCollection services)
        {
            services.AddScoped<IGestorUseCase, GestorUC>();
            return services;
        }

        private static IServiceCollection AddFichaMedicaFeature(this IServiceCollection services)
        {
            services.AddScoped<IFichaMedicaUseCase, FichaMedicaUC>();
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
