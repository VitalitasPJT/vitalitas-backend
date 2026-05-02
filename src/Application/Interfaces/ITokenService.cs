using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string tipoUsuario);
        ClaimsPrincipal? ValidateTokenIgnoringExpiration(string token);
    }
}
