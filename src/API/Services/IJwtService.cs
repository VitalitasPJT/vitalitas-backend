using Domain.Enums;

namespace API.Services
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, UserType tipoUsuario, Guid idAcademia);
    }
}
