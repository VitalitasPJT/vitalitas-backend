using Domain.Features.Token.Entities;

namespace Domain.Features.Token.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void Save(RefreshToken refreshToken);
        RefreshToken? FindByTokenHash(string tokenHash);
        void Revoke(Guid idRefreshToken);
    }
}
