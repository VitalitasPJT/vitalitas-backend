using System.Collections.Concurrent;
using System.Linq;
using Domain.Features.Token.Entities;
using Domain.Features.Token.Interfaces;

namespace API.IntegrationTests.Fakes
{
    public class FakeRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _porHash = new();

        public void Save(RefreshToken refreshToken)
            => _porHash[refreshToken.TokenHash] = refreshToken;

        public RefreshToken? FindByTokenHash(string tokenHash)
            => _porHash.TryGetValue(tokenHash, out var token) ? token : null;

        public void Revoke(Guid idRefreshToken)
            => _porHash.Values.FirstOrDefault(t => t.IdRefreshToken == idRefreshToken)?.Revogar();
    }
}
