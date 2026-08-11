using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateUserWithProfileResponse
    {
        public CreateUserWithProfileResponse(Guid idUsuario, Guid idPerfil, HttpStatus statusHTTP)
        {
            IdUsuario = idUsuario;
            IdPerfil = idPerfil;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdPerfil { get; set; }
    }
}
