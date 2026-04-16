using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vitalitas.Backend.API.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _durationMinutes;

        public JwtService(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"]!;
            _issuer = configuration["Jwt:Issuer"]!;
            _audience = configuration["Jwt:Audience"]!;
            _durationMinutes = int.Parse(configuration["Jwt:DurationInMinutes"]!);
        }

        public string GenerateToken(string userId, string tipoUsuario)
        {
            var role = MapRole(tipoUsuario);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim("IdUsuario", userId),
                new Claim("TipoUsuario", tipoUsuario),
                new Claim("Role", role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_durationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string MapRole(string tipoUsuario)
        {
            return tipoUsuario switch
            {
                "Gestor" => "Administrador",
                "Administrador" => "Administrador",
                "Instrutor" => "Administrador",
                "Aluno" => "Aluno",
                _ => throw new InvalidOperationException($"TipoUsuario '{tipoUsuario}' nao possui mapeamento de Role configurado.")
            };
        }
    }
}
