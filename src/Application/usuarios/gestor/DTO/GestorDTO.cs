using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Application.DTOs
{
    public class GestorDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdAcademia { get; set; }
        public Guid IdGestor { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CPF CPF { get; set; }
    }
}