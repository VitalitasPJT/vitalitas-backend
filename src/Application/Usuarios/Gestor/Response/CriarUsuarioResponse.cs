using System;
using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
    public class CriarUsuarioResponse
    {
        public CriarUsuarioResponse(Guid idUsuario, StatusHTTP statusHTTP)
        {
            IdUsuario = idUsuario;
            Status = statusHTTP;
        }

        public StatusHTTP Status { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
