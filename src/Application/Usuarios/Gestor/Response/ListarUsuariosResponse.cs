using System.Collections.Generic;
using Application.Compartilhado;
using Application.Usuarios.Common.DTO;

namespace Application.Usuarios.Gestor.Response
{
    public class ListarUsuariosResponse
    {
        public ListarUsuariosResponse(List<UsuarioDTO> usuarios, StatusHTTP status)
        {
            Usuarios = usuarios;
            Status = status;
        }

        public List<UsuarioDTO> Usuarios { get; set; }
        public StatusHTTP Status { get; set; }
    }
}
