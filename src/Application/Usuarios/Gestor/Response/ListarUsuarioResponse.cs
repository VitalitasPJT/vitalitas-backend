using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
    public class ListarUsuarioResponse
    {
        public ListarUsuarioResponse(dynamic usuario, StatusHTTP status)
        {
            Usuario = usuario;
            Status = status;
        }

        public dynamic Usuario { get; set; }
        public StatusHTTP Status { get; set; }
    }
}
