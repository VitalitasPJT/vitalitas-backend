using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.Features.Users.Manager.DTO
{
    public class ManagerDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdAcademia { get; set; }
        public Guid IdGestor { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CPF CPF { get; set; }
    }
}