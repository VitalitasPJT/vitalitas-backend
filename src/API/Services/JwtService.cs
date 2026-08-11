using Application.Token.Service;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JwtService : IJwtService, ITokenService
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

        public string GenerateToken(Guid userId, UserType tipoUsuario, Guid idAcademia)
        {
            var role = MapRole(tipoUsuario);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("IdUsuario", userId.ToString()),
                new Claim("IdAcademia", idAcademia.ToString()),
                new Claim("TipoUsuario", tipoUsuario.ToString()),
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

        public ClaimsPrincipal? ValidateTokenIgnoringExpiration(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key))
            };

            try
            {
                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return null;
            }
        }

        // Sem arm default: cobre exaustivamente os valores de UserType hoje. Se um
        // perfil novo for adicionado ao enum sem entrar aqui, o compilador emite
        // CS8509 (switch não exaustivo) em vez de falhar silenciosamente em runtime.
        private static string MapRole(UserType tipoUsuario) => tipoUsuario switch
        {
            UserType.Instrutor => "Instrutor",
            UserType.Aluno => "Aluno",
            UserType.Gestor => "Gestor",
            UserType.Administrador => "Administrador"
        };
    }
}
