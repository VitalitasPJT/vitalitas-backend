using System.Security.Claims;
using Domain.Enums;

namespace Application.Token.Service
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, UserType tipoUsuario, Guid idAcademia);
        ClaimsPrincipal? ValidateTokenIgnoringExpiration(string token);
    }
}
