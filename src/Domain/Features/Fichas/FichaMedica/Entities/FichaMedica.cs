using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Fichas.FichaMedica.Entities
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

        public FichaMedica(Guid idficha, Guid idaluno, string alergia, string restricao, string lesao, string cirurgia, string problemaSaude, string usoMedicamento)
        {
            IdFicha = idficha;
            IdAluno = idaluno;
            Alergia = alergia;
            Restricao = restricao;
            Lesao = lesao;
            Cirurgia = cirurgia;
            ProblemaSaude = problemaSaude;
            UsoMedicamento = usoMedicamento;
        }

        public FichaMedica() {}
    }
}
