using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Vitalitas.Models
{
    [Table("ficha")]
    public class Ficha
    {
        [Key]
        [Required]
        [Column("id_ficha")]
        public int IdFicha { get; set; }

        [Required]
        [Column("nome_ficha")]
        public string NomeFicha { get; set; }

        [Required]
        [Column("observacoes")]
        public string Observacoes { get; set; }

        [Required]
        [Column("id_avaliacao")]
        public int IdAvaliacao { get; set; }
    }

    [Table("treino")]
    public class Treino
    {
        [Required]
        [Key]
        [Column("id_treino")]
        public int IdTreino { get; set; }

        [Column("tipo")]
        [Required]
        public string Tipo { get; set; }

        [Required]
        [Column("nome_treino")]
        public string NomeTreino { get; set; }

        [Required]
        [Column("id_ficha")]
        public int IdFicha { get; set; }
    }

    [Table("exercicio")]
    public class Exercicio
    {
        [Required]
        [Key]
        [Column("id_exercicio")]
        public int IdExercicio { get; set; }

        [Required]
        [Column("nome")]
        public string Nome { get; set; }

        [Required]
        [Column("numero_serie")]
        public Int32 Series { get; set; }

        [Required]
        [Column("numero_repeticao")]
        public Int32 Repeticoes { get; set; }

        [Required]
        [Column("musculo")]
        public string Musculo { get; set; }

        [Required]
        [Column("aparelho")]
        public string Aparelho { get; set; }
    }

    [Table("treino_exercicio")]
    public class TreinoExercicio
    {
        [Key]
        [Required]
        [Column("id_treino")]
        public int IdTreino { get; set; }

        [Required]
        [Column("id_exercicio")]
        public int IdExercicio { get; set; }
    }
}
