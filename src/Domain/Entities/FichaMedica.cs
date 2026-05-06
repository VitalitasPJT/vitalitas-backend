using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FichaMedica
    {
        public Guid IdFicha {  get; private set; }
        public Guid IdAluno { get; private set; }
        public string Alergia { get; private set; }
        public string Restricao { get; private set; }
        public string Lesao { get; private set; }
        public string Cirurgia { get; private set; }
        public string ProblemaSaude { get; private set; }
        public string UsoMedicamento { get; private set; }
    }
}
