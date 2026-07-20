using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        void Save(RefreshToken refreshToken);
        RefreshToken? FindByTokenHash(string tokenHash);
        void Revoke(Guid idRefreshToken);
    }
}
