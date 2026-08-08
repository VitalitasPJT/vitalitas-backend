using System.Collections.Generic;
using Application.Shared;
using Application.Features.Users.Common.DTO;

namespace Application.Features.Users.Manager.Response
{
    public class ListUsersResponse
    {
        public ListUsersResponse(List<UserDTO> usuarios, HttpStatus status)
        {
            Usuarios = usuarios;
            Status = status;
        }

        public List<UserDTO> Usuarios { get; set; }
        public HttpStatus Status { get; set; }
    }
}
