using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateInstructorResponse
    {
        public CreateInstructorResponse(Guid idInstrutor, HttpStatus statusHTTP)
        {
            IdInstrutor = idInstrutor;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdInstrutor { get; set; }
    }
}
