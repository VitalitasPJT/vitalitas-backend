using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vitalitas.Models
{
    [Table("academia")]
    public class Academia
    {
        [Required]
        [Key]
        [Column("id_academia")]
        public int IdAcadenia { get; set; }

        [Required]
        [Column("nome_academia")]
        public string NomeAcademia { get; set; }

        [Required]
        [Column("tipo_academia")]
        public string TipoAcademia { get; set; }

        [Required]
        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Required]
        [Column("email_institucional")]
        public string EmailInstitucional { get; set; }

        [Required]
        [Column("rua")]
        public string Rua { get; set; }

        [Required]
        [Column("[bairro]")]
        public string Bairro { get; set; }

        [Required]
        [Column("[cidade]")]
        public string Cidade { get; set; }

        [Required]
        [Column("[estado]")]
        public string Estado { get; set; }

        [Required]
        [Column("[cep]")]
        public string Cep { get; set; }

        [Required]
        [Column("[quadra]")]
        public string Quadra { get; set; }

    }

    [Table("academia_telefone")]
    public class AcademiaTelefone
    {
        [Required]
        [Key]
        [Column("[id_telefone]")]
        public int IdTelefone { get; set; }

        [Required]
        [Column("[id_acadenia]")]
        public int IdAcademia { get; set; }

        [Required]
        [Column("[telefone]")]
        public string Telefone { get; set; }
    }

    [Table("administracao")]
    public class administracao
    {
        [Required]
        [Key]
        [Column("id_adm")]
        public int IdAdm { get; set; }

        [Required]
        [Column("id_academia")]
        public int IdAcademia {get; set; }
    }
    
}