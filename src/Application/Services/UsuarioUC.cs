using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Settings;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using static Application.DTOs.UsuarioRQ;
using static Application.DTOs.UsuarioRS;

namespace Application.Services
{
    public class UsuarioUC : IUsuarioUseCase
    {
        private readonly IUsuario _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenSettings _refreshTokenSettings;

        public UsuarioUC(
            IUsuario usuarioRepository,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository,
            RefreshTokenSettings refreshTokenSettings)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenSettings = refreshTokenSettings;
        }

        public AdicionarLogResponse AdicionarLog(Guid idusuario, LogAtividade log)
        {
            throw new NotImplementedException();
        }

        public AtualizarDadosResponse AtualizarDados(Guid idusuario, dynamic valor, string atributo)
        {
            throw new NotImplementedException();
        }

        public CriarUsuarioResponse CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario)
        {
            var idUsuario = _usuarioRepository.CriarUsuario(nome, email, senha, quadra, rua, bairro, cidade, estado, cep, dataNascimento, cpf, tipoUsuario);
            return new CriarUsuarioResponse(idUsuario, new StatusHTTP("Usuário criado com sucesso", 201, true));
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
            var response = new LoginResponse(usuario.TipoUsuario, usuario.IdUsuario, usuario.Flag, status);
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

        public ObterLogsResponse ObterLogs(Guid idusuario)
        {
            throw new NotImplementedException();
        }

        public TrocarSenhaResponse TrocarSenha(Guid idusaurio, string novasenha)
        {
            var usuario = _usuarioRepository.TrocarSenha(idusaurio, novasenha);
            if (usuario == null)
            {
                throw new Exception("Erro ao trocar senha");
            }
            var status = new StatusHTTP("Senha trocada com sucesso", 200, true);
            var response = new TrocarSenhaResponse(status);
            return response;
        }
        //throw new NotImplementedException();
    }
}