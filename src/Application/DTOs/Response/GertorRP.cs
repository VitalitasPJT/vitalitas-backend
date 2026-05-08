using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.Response
{
    public class GertorRP
    {
        public class CriarUsuarioResponse
        {
            private StatusHTTP statusHTTP;

            public CriarUsuarioResponse(Guid idUsuario, StatusHTTP statusHTTP)
            {
                IdUsuario = idUsuario;
                this.statusHTTP = statusHTTP;
            }

            public bool Sucesso { get; set; }
            public string Mensagem { get; set; }
            public Guid IdUsuario { get; set; }
        }


    }
}