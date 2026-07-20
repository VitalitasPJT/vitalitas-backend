using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TelefoneUsuario
    {
        public Guid IdTelefone { get; private set; }
        public Guid IdUsuario { get; private set; }
        public string Telefone { get; private set; }
    }

}