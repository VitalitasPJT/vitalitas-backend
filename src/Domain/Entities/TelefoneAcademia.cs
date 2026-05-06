using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TelefoneAcademia
    {
        public Guid IdTelefone { get; private set; }
        public Guid IdAcademia { get; private set; }
        public string Telefone { get; private set; }
    }
}
