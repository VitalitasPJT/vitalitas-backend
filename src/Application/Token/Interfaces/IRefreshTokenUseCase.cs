using static Application.Usuarios.Common.Response.UsuarioRS;

namespace Application.Token.Interfaces
{
    public interface IRefreshTokenUseCase
    {
        RefreshResponse Refresh(string accessToken, string refreshToken);
    }
}
