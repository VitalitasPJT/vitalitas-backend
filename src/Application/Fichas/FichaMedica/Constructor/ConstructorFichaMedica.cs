using System;

namespace Application.Fichas.FichaMedica.Constructor
{
    public class ConstructorFichaMedica
    {
        public Guid IdFicha { get; private set; }
        public Guid IdAluno { get; private set; }
        public string Alergia { get; private set; }
        public string Restricao { get; private set; }
        public string Lesao { get; private set; }
        public string Cirurgia { get; private set; }
        public string ProblemaSaude { get; private set; }
        public string UsoMedicamento { get; private set; }

        public ConstructorFichaMedica(Guid idAluno, string alergia, string restricao, string lesao, string cirurgia, string problemaSaude, string usoMedicamento)
        {
            IdFicha = Guid.NewGuid();
            IdAluno = idAluno;
            Alergia = alergia;
            Restricao = restricao;
            Lesao = lesao;
            Cirurgia = cirurgia;
            ProblemaSaude = problemaSaude;
            UsoMedicamento = usoMedicamento;
        }
    }
}