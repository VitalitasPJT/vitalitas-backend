using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdAcademia { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Ativo { get; set; }
    }
}