using static Application.DTOs.UsuarioRS;

namespace Application.Interfaces
{
    public interface IRefreshTokenUseCase
    {
        RefreshResponse Refresh(string accessToken, string refreshToken);
    }
}
