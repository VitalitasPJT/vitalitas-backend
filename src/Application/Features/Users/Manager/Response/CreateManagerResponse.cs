using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateManagerResponse
    {
        public CreateManagerResponse(Guid idGestor, HttpStatus statusHTTP)
        {
            IdGestor = idGestor;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdGestor { get; set; }
    }
}
