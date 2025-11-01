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
    
    

    public class Login
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
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
        public DateTime Data_Nascimento { get; set; }
        
        [Required]
        public string Responsavel { get; set; }
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

    
}
