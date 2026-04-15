using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Domain.Entities
{
    public class Ficha
    {
        public Guid IdFicha { get; private set; }
        public Guid IdAvaliacao { get; private set; }
        public string NomeFicha { get; private set; }
        public string Observacoes { get; private set; }
    }
}
