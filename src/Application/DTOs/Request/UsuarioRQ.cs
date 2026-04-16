using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.DTOs
{
    public class UsuarioRQ
    {
        public class LoginRequest
        {
            public string Email { get; set;}
            public string Senha { get; set;}
            public string? Fluxo { get; set; }
        }

        public class TrocarSenhaRequest
        {
            public Guid IdUsuario { get; set;}
            public string NovaSenha { get; set;}
        }

        public class CriarUsuarioRequest
        {
            public string Nome { get; set;}
            public string Email { get; set;}
            public string Senha { get; set;}
            public string Quadra { get; set;}
            public string Rua { get; set;}
            public string Bairro { get; set;}
            public string Cidade { get; set;}
            public string Estado { get; set;}
            public string Cep { get; set;}
            public DateOnly DataNascimento { get; set;}
            public string Cpf { get; set;}
            public TipoUsuario TipoUsuario { get; set;}
        }
    }
}
