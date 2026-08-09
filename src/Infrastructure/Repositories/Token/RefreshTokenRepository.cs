using Domain.Features.Token.Entities;
using Domain.Features.Token.Interfaces;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Token
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Save(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
        }

        public RefreshToken? FindByTokenHash(string tokenHash)
        {
            return _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefault(rt => rt.TokenHash == tokenHash);
        }

        public void Revoke(Guid idRefreshToken)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.IdRefreshToken == idRefreshToken);
            if (refreshToken == null)
                return;

            refreshToken.Revogar();
            _context.SaveChanges();
        }
    }
}
