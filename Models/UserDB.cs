using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Vitalitas.Models
{
    [Table("USUARIO")]
    public class UsuarioC
    {
        [Key]
        [Column("ID")]
        public required string Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Usuario { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }

        [Required]
        public required string Telefone { get; set; }

        [Required]
        public required string Tipo { get; set; }
    }

    public class LoginUser
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }

    public class LoginAdm
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Id_Acesso {  get; set; }
    }

    public class LoginResponseUser
    {
        public string Sucesso { get; set; }
        public string Tipo { get; set; }
        public string Mensagem { get; set; }
        public string Id { get; set; }
    }

    public class LoginResponseAdm
    {
        public string Sucesso { get; set; }
        public string Id { get; set; }
    }

    public class ProfessorDados
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Id { get; set; }
    }

    public class Responser<T>
    {
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public T Data { get; set; }

        public Responser(string mensagem, bool sucesso, T data = default)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
            Data = data;
        }
    }


    [Table("ALUNO")]
    public class Aluno
    {
        [Required]
        public string Id_Usuario { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Data_Inscricao { get; set; }

        public string Objetivo { get; set; }

        [Key]
        [Required]
        public string Cpf { get; set; }

        [Required]
        public DateTime Data_Nascimento { get; set; }
        
        [Required]
        public string Responsavel { get; set; }

        [Required]
        public string Sexo { get; set; }
    }

    [Table("PROFESSOR")]
    public class Professor
    {
        [Required]
        public string Id_Usuario { get; set; }

        [Key]
        [Required]
        public long Cref { get; set; }
  
    }

    [Table("ADMINISTRADOR")]
    public class Administrador
    {
        [Required]
        public string Id_Usuario { get; set; }

        [Required]
        public string Nivel { get; set; }
        
        [Key]
        [Required]
        public long Registro { get; set; }

        [Required]
        public string Id_Acesso { get; set; }
    }
}
