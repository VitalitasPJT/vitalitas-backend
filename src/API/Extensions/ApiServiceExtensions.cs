using API.Services;
using Application.Token.Service;
using Domain.Features.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITokenService, JwtService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ITenantContext, CurrentTenantService>();

            return services;
        }
    }
}
