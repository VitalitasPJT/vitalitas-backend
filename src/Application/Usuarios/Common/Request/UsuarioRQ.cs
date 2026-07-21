using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Usuarios.Common.Request
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
            public required int Acao { get; set; }
            public required string DispositivoLogado { get; set; }
            public required string Localizacao { get; set; }
        }

        public class ObterTipoUsuarioRequest
        {
            public Guid IdUsuario { get; set; }
        }
    }
}
