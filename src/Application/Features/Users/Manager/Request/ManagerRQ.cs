using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Users.Manager.Request
{
    public class ManagerRQ
    {
        public class CreateUserRequest
        {
            public Guid IdAcademia { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public DateOnly DataNascimento { get; set; }
            public string Cpf { get; set; }
            public UserType TipoUsuario { get; set; }
            public string Quadra { get; set; }
            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Cep { get; set; }
        }
        
        public class CreateMemberRequest
        {
            public Guid IdUsuario { get; set; }
            public Guid IdInstrutor { get; set; }
            public Guid IdContrato { get; set; }
            public required string Objetivo { get; set; }
        }

        public class CreateInstructorRequest
        {
            public Guid IdUsuario { get; set; }
            public string CREF { get; set; }
        }

    }
}