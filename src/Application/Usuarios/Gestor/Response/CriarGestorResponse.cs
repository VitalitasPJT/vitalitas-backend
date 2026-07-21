using System;
using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
    public class CriarGestorResponse
    {
        public CriarGestorResponse(Guid idGestor, StatusHTTP statusHTTP)
        {
            IdGestor = idGestor;
            Status = statusHTTP;
        }

        public StatusHTTP Status { get; set; }
        public Guid IdGestor { get; set; }
    }
}
