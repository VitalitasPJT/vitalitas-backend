using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Shared;
using Application.Token.Interfaces;
using Application.Token.Service;
using Application.Token.Settings;
using Domain.Features.Token.Entities;
using Domain.Features.Token.Interfaces;
using static Application.Features.Users.Common.Response.UserRS;

namespace Application.Token.UseCases
{
    public class RefreshTokenUC : IRefreshTokenUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenSettings _settings;

        public RefreshTokenUC(
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            RefreshTokenSettings settings)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _settings = settings;
        }

        public RefreshResponse Refresh(string accessToken, string refreshToken)
        {
            // 1. Validate JWT — sig/issuer/audience enforced, expiry ignored
            var principal = _tokenService.ValidateTokenIgnoringExpiration(accessToken);
            if (principal == null)
                throw new UnauthorizedAccessException();

            // 2. Extract identity from verified claims — never from request body
            var userId = principal.FindFirst("IdUsuario")?.Value;
            var tipoUsuario = principal.FindFirst("TipoUsuario")?.Value;
            var tenantId = principal.FindFirst("TenantId")?.Value;
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(tipoUsuario) || string.IsNullOrWhiteSpace(tenantId))
                throw new UnauthorizedAccessException();

            // 3. Hash the incoming token and look up by hash only
            var tokenHash = ComputeHash(refreshToken);
            var stored = _refreshTokenRepository.FindByTokenHash(tokenHash);
            if (stored == null)
                throw new UnauthorizedAccessException();

            // 4–6. Validate ownership, expiry, and revocation
            if (stored.IdUsuario.ToString() != userId)
                throw new UnauthorizedAccessException();

            if (stored.DataExpiracao < DateTime.UtcNow)
                throw new UnauthorizedAccessException();

            if (stored.Revogado)
                throw new UnauthorizedAccessException();

            // 7–8. Generate new token pair
            var newAccessToken = _tokenService.GenerateToken(userId, tipoUsuario, tenantId);
            var newRawToken = GenerateRawToken();
            var newTokenHash = ComputeHash(newRawToken);

            // 9. Rotate — revoke old refresh token
            _refreshTokenRepository.Revoke(stored.IdRefreshToken);

            // 10. Persist new refresh token (hash only)
            _refreshTokenRepository.Save(new RefreshToken(
                Guid.NewGuid(),
                newTokenHash,
                DateTime.UtcNow.AddDays(_settings.RefreshTokenDurationInDays),
                false,
                stored.IdUsuario
            ));

            var status = new HttpStatus("Token renovado com sucesso", 200, true);
            return new RefreshResponse(newAccessToken, newRawToken, status);
        }

        private static string GenerateRawToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }

        private static string ComputeHash(string rawToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}
