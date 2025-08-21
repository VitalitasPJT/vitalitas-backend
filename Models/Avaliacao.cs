using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Models
{
    [Table("AVALIACAO")]
    public class Avaliacao
    {
        [Key]
        [Required]
        public string Id_Avaliacao { get; set; }

        [Required]
        public string Id_Aluno { get;set; }

        [Required]
        public string Id_Professor { get; set; }

        [Required]
        public DateOnly Data { get; set; }

        [Required]
        public TimeOnly Hora { get; set; }

        [Required]
        public double Peso { get; set; }

        [Required]
        public int Idade { get; set; }

        [Required]
        public double Altura { get; set; }
    }

    [Table("PERIMETRO_AVALIACAO")]
    public class Perimetro
    {
        [Key]
        [Required]
        public string Id_Avaliacao { get; set; }

        [Required]
        public double Perna_E { get; set; }

        [Required]
        public double Perna_D { get; set; } 

        [Required]
        public double Coxa_E { get; set; }

        [Required]
        public double Coxa_D { get; set; }

        [Required]
        public double Braco_E { get; set; }

        [Required]
        public double Braco_D { get; set; }

        [Required]
        public double Quadril { get; set; }

        [Required]
        public double Abdomen { get; set; }

        [Required]
        public double Deltoide { get; set; }

        [Required]
        public double Cintura { get; set; }

        [Required]
        public double Torax { get; set; }
    }

    [Table("CUTANEAS_AVALIACAO")]
    public class Cutaneas
    {
        [Key]
        [Required]
        public string Id_Avaliacao { get; set; }

        [Required]
        public double Tr { get; set; }

        [Required]
        public double Cx { get; set; }

        [Required]
        public double Si { get; set; }

        [Required]
        public double Ab { get; set; }

        [Required]
        public double Ax { get; set; }

        [Required]
        public double Pt { get; set; }

        [Required]
        public double Se { get; set; }

        [Required]
        public double Paturrilha { get; set; }

        [Required]
        public double Umero { get; set; }

        [Required]
        public double Femur { get; set; }
    }

    [Table("RESULTADO")]
    public class Resultado
    {
        [Key]
        [Required]
        public string Id_Avaliacao { get; set; }

        [Required]
        public double Imc { get; set; }

        [Required]
        public double Soma_Das_Dobras { get; set; }

        [Required]
        public double Densidade_Corporal { get; set; }

        [Required]
        public double Percentual_De_Gordura { get; set; }

        [Required]
        public double Massa_Gorda { get; set; }

        [Required]
        public double Percentual_De_Massa_Magra { get; set; }

        [Required]
        public double Massa_Magra { get; set; }

        public Resultado() { }

        public Resultado(string id, double imc, double somadobras, double densidade, double pgordura, double massagorda, double pmagra, double massamagra)
        {
            Id_Avaliacao = id;
            Imc = imc;
            Soma_Das_Dobras = somadobras;
            Densidade_Corporal = densidade;
            Percentual_De_Gordura = pgordura;
            Massa_Gorda = massagorda;
            Percentual_De_Massa_Magra = pmagra;
            Massa_Magra = massamagra;
        }
    }

    public class Calcular
    {
        public string Sexo { get; set; }
        public string Id_Avaliacao { get; set; }
    }
}
