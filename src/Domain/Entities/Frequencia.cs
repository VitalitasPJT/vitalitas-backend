using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Frequencia
    {
        public Guid IdFrequencia { get; private set; }
        public Guid IdAluno { get; private set; }
        public int TempoTreinoMinutos { get; private set; }
        public DateTime Data {  get; private set; }
    }
}
