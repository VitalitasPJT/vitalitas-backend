using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Features.Users.Member.DTO
{
    public class MemberDTO
    {
        public Guid IdAluno { get; set; }
        public Guid IdAcademia { get; set; }
        public Guid IdUsuario { get; set; }
        public UserType TipoUsuario { get; set; }
        public required string Objetivo { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string StatusPagamento { get; set; }

    }
}