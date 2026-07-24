using System;
using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
    public class CriarInstrutorResponse
    {
        public CriarInstrutorResponse(Guid idInstrutor, StatusHTTP statusHTTP)
        {
            IdInstrutor = idInstrutor;
            Status = statusHTTP;
        }

        public StatusHTTP Status { get; set; }
        public Guid IdInstrutor { get; set; }
    }
}
