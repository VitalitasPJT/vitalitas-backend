using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.ValueObjects;
using static Application.DTOs.UsuarioRP;

namespace Application.Services
{
    public class UsuarioUC : IUsuarioUseCase
    {
        private readonly IUsuario _usuarioRepository;
        public UsuarioUC(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public LoginResponse Login(string email, string senha)
        {
            var usuario = _usuarioRepository.Login(email, senha);
            if (usuario == null)
            {
                throw new Exception("Usuário ou senha inválidos");
            }
            var status = new StatusHTTP("Login realizado com sucesso", 200, true);
            var response = new LoginResponse(usuario.TipoUsuario, usuario.IdUsuario, usuario.Flag, status);
            return response;
        }

        public TrocarSenhaResponse TrocarSenha(Guid idusaurio, string novasenha)
        {
            throw new NotImplementedException();
        }

    }
}