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
        public LoginResponse Login(Email email, string senha)
        {
            throw new NotImplementedException();
        }

        public TrocarSenhaResponse TrocarSenha(Guid idusaurio, string novasenha)
        {
            throw new NotImplementedException();
        }
    }
}