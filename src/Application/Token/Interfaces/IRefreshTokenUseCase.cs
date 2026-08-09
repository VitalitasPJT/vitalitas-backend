using static Application.Features.Users.Common.Response.UserRS;

namespace Application.Token.Interfaces
{
    public interface IRefreshTokenUseCase
    {
        RefreshResponse Refresh(string accessToken, string refreshToken);
    }
}
