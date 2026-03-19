using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.DTOs
{
    public class UsuarioRQ
    {
        public class LoginRequest
        {
            public Email Email { get; set;}
            public string Senha { get; set;}
        }

        public class TrocarSenhaRequest
        {
            public Guid IdUsuario { get; set;}
            public string NovaSenha { get; set;}
        }
    }
}