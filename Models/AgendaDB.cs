using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Models
{
    [Table("AGENDA")]
    public class Agenda
    {
        [Key]
        [Required]
        public string Id_Agenda { get; set; }

        [Required]
        public string Id_Aluno { get; set; }

        [Required]
        public string Id_Professor { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public TimeSpan Hora { get; set; }

        [Required]
        public DateTime Data { get; set; }
    }
}
