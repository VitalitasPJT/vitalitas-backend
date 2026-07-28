namespace Domain.Features.Shared.Entities
{
    public class Avaliacao
    {
        public Guid IdAvaliacao { get; set; }
        public Guid IdAcademia { get; set; }
        public Guid IdProfessor { get; set; }
        public Guid IdAluno { get; set; }

        // Dados da Avaliação
        public DateOnly Data { get; set; }
        public TimeOnly Hora { get; set; }
        public double Peso { get; set; }
        public string? Sexo { get; set; }
        public double Altura { get; set; }
        public int Idade { get; set; }
        public double Glicemia { get; set; }
        public string Pa { get; set; }

        // Medidas Cutâneas (Dobras)
        public double Densidade { get; set; }
        public double Ax { get; set; }
        public double Pt { get; set; }
        public double Se { get; set; }
        public double Tr { get; set; }
        public double Ab { get; set; }
        public double Si { get; set; }
        public double Ub { get; set; }
        public double Ur { get; set; }
        public double Rt { get; set; }
        public double Pr { get; set; }
        public double Px { get; set; }

        // Medidas Ósseas (Antropometria)
        public double Femur { get; set; }

        // Perímetros
        public double Abdomen { get; set; }
        public double Torax { get; set; }
        public double Quadril { get; set; }
        public double BracoD { get; set; }
        public double BracoE { get; set; }
        public double CoxaD { get; set; }
        public double CoxaE { get; set; }
        public double PernaD { get; set; }
        public double PernaE { get; set; }
        public double Deltoide { get; set; }
        public double Peitoral { get; set; }

        // Resultados/Composição Corporal
        public double PMagro { get; set; }
        public double PGordo { get; set; }
        public double POsseo { get; set; }
        public double PViscera { get; set; }
    }
}
