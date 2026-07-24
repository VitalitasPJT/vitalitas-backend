using API.Services;
using Application.Token.Service;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITokenService, JwtService>();

            return services;
        }
    }
}
