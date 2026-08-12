using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace API.IntegrationTests
{
    // Espelha os claims exatos que JwtService.GenerateToken produz (Sub,
    // IdUsuario, IdAcademia, TipoUsuario, Role, Jti) — mas permite controlar a
    // expiração manualmente, o que GenerateToken não expõe. "Token expirado" é
    // justamente o cenário que estes testes precisam simular.
    public static class TestJwt
    {
        public static string Mint(UserType tipoUsuario, Guid idUsuario, Guid idAcademia, TimeSpan expiresIn)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthPipelineWebApplicationFactory.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),
                new Claim("IdUsuario", idUsuario.ToString()),
                new Claim("IdAcademia", idAcademia.ToString()),
                new Claim("TipoUsuario", tipoUsuario.ToString()),
                new Claim("Role", tipoUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: AuthPipelineWebApplicationFactory.JwtIssuer,
                audience: AuthPipelineWebApplicationFactory.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.Add(expiresIn),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Valid(UserType tipoUsuario, Guid idUsuario, Guid idAcademia)
            => Mint(tipoUsuario, idUsuario, idAcademia, TimeSpan.FromMinutes(15));

        public static string Expired(UserType tipoUsuario, Guid idUsuario, Guid idAcademia)
            => Mint(tipoUsuario, idUsuario, idAcademia, TimeSpan.FromMinutes(-5));
    }
}
