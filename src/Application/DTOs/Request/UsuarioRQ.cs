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
            public required string Email { get; set; }
            public required string Senha { get; set; }
        }

        public class RefreshRequest
        {
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
        }

        public class AdicionarLogRequest
        {
            public Guid IdUsuario { get; set; }
            public required string Acao { get; set; }
        }

        public class ObterTipoUsuarioRequest
        {
            public Guid IdUsuario { get; set; }
        }



        /*public class TrocarSenhaRequest
        {
            public Guid IdUsuario { get; set; }
            public string NovaSenha { get; set; }
        }

        public class CriarUsuarioRequest
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string Quadra { get; set; }
            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Cep { get; set; }
            public DateOnly DataNascimento { get; set; }
            public string Cpf { get; set; }
            public TipoUsuario TipoUsuario { get; set; }
        }

        public class AtualizarDadosRequest
        {
            public Guid IdUsuario { get; set; }
            public string Atributo { get; set; }
            public object Valor { get; set; }
        }

        public class DesativarRequest
        {
            public Guid IdUsuario { get; set; }
        }

        public class AtivarRequest
        {
            public Guid IdUsuario { get; set; }
        }

        public class ObterLogsRequest
        {
            public Guid IdUsuario { get; set; }
        }*/




    }
}
