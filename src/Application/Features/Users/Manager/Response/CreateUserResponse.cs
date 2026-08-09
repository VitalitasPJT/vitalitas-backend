using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateUserResponse
    {
        public CreateUserResponse(Guid idUsuario, HttpStatus statusHTTP)
        {
            IdUsuario = idUsuario;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
