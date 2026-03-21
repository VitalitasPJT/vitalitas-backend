namespace Vitalitas.Backend.API.Services.JwtService
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userEmail);
    }
}
