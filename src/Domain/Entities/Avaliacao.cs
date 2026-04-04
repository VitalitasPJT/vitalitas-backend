using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Backend.Domain.Entities
{
    [Table("agenda")]
    public class Agenda
    {
        [Key]
        [Required]
        [Column("[id_agenda]")]
        public int IdAgenda { get; set; }

        [Required]
        [Column("[id_professor]")]
        public int IdProfessor { get; set; }

        [Required]
        [Column("[data]")]
        public DateTime Data { get; set; }

        [Required]
        [Column("[status]")]
        public string Status { get; set; }
    }

    [Table("avaliacao")]
    public class Avaliacao
    {
        [Key]
        [Column("id_avaliacao")]
        [Required]
        public int IdAvaliacao { get; set; }

        [Column("id_professor")]
        [Required]
        public int IdProfessor { get; set; }

        [Column("id_aluno")]
        [Required]
        public int IdAluno { get; set; }

        // Dados da Avaliação
        [Column("data")]
        [Required]
        public DateOnly Data { get; set; }

        [Column("hora")]
        [Required]
        public TimeOnly Hora { get; set; }

        [Column("peso")]
        [Required]
        public double Peso { get; set; }

        [Column("sexo")]
        [Required]
        public string? Sexo { get; set; }

        [Column("altura")]
        [Required]
        public double Altura { get; set; }

        [Column("idade")]
        [Required]
        public int Idade { get; set; }

        [Column("glicemia")]
        [Required]
        public double Glicemia { get; set; }

        [Column("pa")]
        [Required]
        public string Pa { get; set; }

        // Medidas Cutâneas (Dobras)
        [Column("densidade")]
        [Required]
        public double Densidade { get; set; }

        [Column("ax")]
        [Required]
        public double Ax { get; set; }

        [Column("pt")]
        [Required]
        public double Pt { get; set; }

        [Column("se")]
        [Required]
        public double Se { get; set; }

        [Column("tr")]
        [Required]
        public double Tr { get; set; }

        [Column("ab")]
        [Required]
        public double Ab { get; set; }

        [Column("si")]
        [Required]
        public double Si { get; set; }

        [Column("ub")]
        [Required]
        public double Ub { get; set; }

        [Column("ur")]
        [Required]
        public double Ur { get; set; }

        [Column("rt")]
        [Required]
        public double Rt { get; set; }

        [Column("pr")]
        [Required]
        public double Pr { get; set; }

        [Column("px")]
        [Required]
        public double Px { get; set; }

        // Medidas Ósseas (Antropometria)
        [Column("femur")]
        [Required]
        public double Femur { get; set; }

        // Perímetros
        [Column("abdomen")]
        [Required]
        public double Abdomen { get; set; }

        [Column("torax")]
        [Required]
        public double Torax { get; set; }

        [Column("quadril")]
        [Required]
        public double Quadril { get; set; }

        [Column("braco_d")]
        [Required]
        public double BracoD { get; set; }

        [Column("braco_e")]
        [Required]
        public double BracoE { get; set; }

        [Column("coxa_d")]
        [Required]
        public double CoxaD { get; set; }

        [Column("coxa_e")]
        [Required]
        public double CoxaE { get; set; }

        [Column("perna_d")]
        [Required]
        public double PernaD { get; set; }

        [Column("perna_e")]
        [Required]
        public double PernaE { get; set; }

        [Column("deltoide")]
        [Required]
        public double Deltoide { get; set; }

        [Column("peitoral")]
        [Required]
        public double Peitoral { get; set; } 

        // Resultados/Composição Corporal
        [Column("p_magro")]
        [Required]
        public double PMagro { get; set; }

        [Column("p_gordo")]
        [Required]
        public double PGordo { get; set; }

        [Column("p_osseo")]
        [Required]
        public double POsseo { get; set; }

        [Column("p_viscera")]
        [Required]
        public double PViscera { get; set; } 
        
    }

}
