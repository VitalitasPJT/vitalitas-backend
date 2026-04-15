using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
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

        public CriarUsuarioResponse CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario)
        {
            var idUsuario = _usuarioRepository.CriarUsuario(nome, email, senha, quadra, rua, bairro, cidade, estado, cep, dataNascimento, cpf, tipoUsuario);
            return new CriarUsuarioResponse(idUsuario, new StatusHTTP("Usuário criado com sucesso", 201, true));
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