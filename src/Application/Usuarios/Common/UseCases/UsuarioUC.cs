using System.Security.Cryptography;
using System.Text;
using Application.Compartilhado;
using Application.Usuarios.Common.Interfaces;
using Application.Token.Service;
using Application.Token.Settings;
using Domain.Features.Token.Entities;
using Domain.Features.Usuarios.Common.Interfaces;
using Domain.Features.Token.Interfaces;
using static Application.Usuarios.Common.Response.UsuarioRS;

namespace Application.Usuarios.Common.UseCases
{
    public class UsuarioUC : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        public UsuarioUC(
            IUsuarioRepository usuarioRepository,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            RefreshTokenSettings refreshTokenSettings)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenSettings = refreshTokenSettings;
        }

        public LoginResponse Login(string email, string senha)
        {
            var usuario = _usuarioRepository.Login(email, senha);
            if (usuario == null)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var accessToken = _tokenService.GenerateToken(usuario.IdUsuario.ToString(), usuario.TipoUsuario.ToString());

            var rawRefreshToken = GenerateRawToken();
            var tokenHash = ComputeHash(rawRefreshToken);

            _refreshTokenRepository.Save(new RefreshToken(
                Guid.NewGuid(),
                tokenHash,
                DateTime.UtcNow.AddDays(_refreshTokenSettings.DurationInDays),
                false,
                usuario.IdUsuario
            ));

            var status = new StatusHTTP("Login realizado com sucesso", 200, true);
            var response = new LoginResponse(usuario.TipoUsuario, usuario.IdUsuario, usuario.IdAcademia, usuario.Flag, status);
            response.Token = accessToken;
            response.RefreshToken = rawRefreshToken;
            return response;
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

        public RefreshResponse RefreshToken(string accessToken, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public AdicionarLogResponse AdicionarLog(Guid idusuario, int acao, string dispositivoLogado, string localizacao)
        {
            var logAtividade = _usuarioRepository.RegistrarAcao(idusuario, acao, dispositivoLogado, localizacao);
            if (logAtividade == null)
                throw new Exception("Erro ao registrar ação");

            var status = new StatusHTTP("Ação registrada com sucesso", 200, true);
            var response = new AdicionarLogResponse(logAtividade, status);
            return response;
        }

        public ObterTipoUsuarioResponse ObterTipoUsuario(Guid idUsuario)
        {
            var tipoUsuario = _usuarioRepository.GetTipoUsuario(idUsuario);
            if (tipoUsuario == null)
                throw new Exception("Usuário não encontrado");

            var status = new StatusHTTP("Tipo de usuário obtido com sucesso", 200, true);
            var response = new ObterTipoUsuarioResponse
            {
                TipoUsuario = tipoUsuario.Value,
                Status = status
            };
            return response;
        }
    }
}