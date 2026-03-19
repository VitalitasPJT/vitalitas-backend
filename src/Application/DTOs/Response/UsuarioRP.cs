using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class UsuarioRP
    {
        public class LoginResponse
        {
            public TipoUsuario TipoUsuario { get; set; }
            public Guid IdUsuario { get; set;}
            public bool Flag {get; set;}
            public StatusHTTP Status { get; set;}
            public LoginResponse(TipoUsuario tipoUsuario, Guid idUsuario, bool flag, StatusHTTP status)
            {
                this.TipoUsuario = tipoUsuario;
                this.IdUsuario = idUsuario;
                this.Flag = flag;
                this.Status = status;
            }
        }

        public class TrocarSenhaResponse
        {
        
            public StatusHTTP status { get; set;}
            public TrocarSenhaResponse(StatusHTTP status)
            {
                this.status = status;
            }
        }
    }
}