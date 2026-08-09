using System;
using Application.Shared;

namespace Application.Features.Users.Manager.Response
{
    public class CreateEmployeeResponse
    {
        public CreateEmployeeResponse(Guid idFuncionario, HttpStatus statusHTTP)
        {
            IdFuncionario = idFuncionario;
            Status = statusHTTP;
        }

        public HttpStatus Status { get; set; }
        public Guid IdFuncionario { get; set; }
    }
}
