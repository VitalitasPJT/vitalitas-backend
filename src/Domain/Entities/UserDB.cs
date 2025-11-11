using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Vitalitas.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Column("[id_usuario]")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("[nome]")]
        public string Nome { get; set; }

        [Required]
        [Column("[cpf]")]
        public string Cpf { get; set; }

        [Required]
        [Column("[email]")]
        public string Email { get; set; }

        [Required]
        [Column("[senha]")]
        public string Senha { get; set; }

        [Required]
        [Column("[data_nascimento]")]
        public DateOnly DataNascimento { get; set; }

        [Required]
        [Column("[rua]")]
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

        [Required]
        [Column("[tipo_usuario]")]
        public string TipoUsuario { get; set; }

    }

    [Table("usuario_telefone")]
    public class UsarioTelefone
    {
        [Required]
        [Key]
        [Column("[id_telefone]")]
        public int IdTelefone { get; set; }

        [Required]
        [Column("[id_usuario]")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("[telefone]")]
        public string Telefone { get; set; }
    }

    [Table("administrador")]
    public class Administrador
    {
        [Key]
        [Required]
        [Column("[id_adm]")]
        public int IdAdm { get; set; }

        [Required]
        [Column("[id_usuario]")]
        public int IdUsuario { get; set; }
    }

    [Table("aluno")]
    public class Aluno
    {
        [Required]
        [Key]
        [Column("[id_aluno]")]
        public int IdAluno { get; set; }

        [Required]
        [Column("[id_usuario]")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("[objetivo]")]
        public string Objetivo { get; set; }
    }

    [Table("professor")]
    public class Professor
    {
        [Required]
        [Key]
        [Column("[id_professor]")]
        public int IdProfessor { get; set; }

        [Required]
        [Column("[id_usuario]")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("[cref]")]
        public string Cref { get; set; }
  
    }

    
}
