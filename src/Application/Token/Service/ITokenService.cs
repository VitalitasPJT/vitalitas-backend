using System.Security.Claims;

namespace Application.Token.Service
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string tipoUsuario, string tenantId);
        ClaimsPrincipal? ValidateTokenIgnoringExpiration(string token);
    }
}
