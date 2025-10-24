using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Models
{
    [Table("FICHA_DE_TREINO")]
    public class FichaDeTreino
    {
        [Key]
        [Required]
        public string Id_Ficha { get; set; }

        [Required]
        public string Id_Aluno { get; set; }

        [Required]
        public string Responsavel { get; set; }

        [Required]
        public DateTime Data_Criacao { get; set; }

        [Required]
        public DateTime Data_Validade { get; set; }
        [Required]
        public string Nome { get; set; }

        public string Observacoes { get; set; }
    }

    [Table("TREINO")]
    public class Treino
    {

        [Required]
        public string Id_Ficha_Treino { get; set; }

        [Required]
        [Key]
        public string Id_Treino { get; set; }

        [Required]
        public string Nome { get; set; }
    }

    [Table("TREINO_EXERCICIO")]
    public class TreinoExercicio
    {
        [Key]
        [Required]
        public string Id_Treino { get; set; }

        [Required]
        public string Id_Exercicio { get; set; }

        public Int32 Series { get; set; }
        public Int32 Repeticoes { get; set; }
        public string Aparelho { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Musculo { get; set; }
    }
}
