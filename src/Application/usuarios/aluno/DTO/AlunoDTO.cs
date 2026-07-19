using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class AlunoDTO
    {
        public Guid IdAluno { get; set; }
        public Guid IdAcademia { get; set; }
        public Guid IdUsuario { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public required string Objetivo { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string StatusPagamento { get; set; }

    }
}