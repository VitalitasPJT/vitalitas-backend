using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateMemberResponse
    {
        public CreateMemberResponse(Guid idAluno, HttpStatus statusHTTP)
        {
            IdAluno = idAluno;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdAluno { get; set; }
    }
}
