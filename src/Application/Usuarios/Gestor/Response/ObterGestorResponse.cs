using Application.Compartilhado;
using Application.Usuarios.Gestor.DTO;

namespace Application.Usuarios.Gestor.Response
{
    public class ObterGestorResponse
    {
        public ObterGestorResponse(GestorDTO? gestor, StatusHTTP status)
        {
            Gestor = gestor;
            Status = status;
        }

        public GestorDTO? Gestor { get; set; }
        public StatusHTTP Status { get; set; }
    }
}
