namespace API.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string tipoUsuario, string tenantId);
    }
}
