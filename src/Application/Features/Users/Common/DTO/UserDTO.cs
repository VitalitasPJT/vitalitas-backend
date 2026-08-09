using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Users.Common.DTO
{
    public class UserDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdAcademia { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public UserType TipoUsuario { get; set; }
        public bool Ativo { get; set; }
    }
}