using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; private set; }
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Quadra { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public string Senha { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public CPF CPF { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }
        public bool Flag { get; private set; }
        /// <summary>
        /// Lista de IDs de logs associados ao usuário.
        /// </summary>
        //public List<Guid> IdLog { get; private set; }

        public Usuario(Guid idUsuario, Nome nome, Email email, string quadra, string rua, string bairro, string cidade, string estado, string cep, string senha, DateOnly dataNascimento, CPF cpf, TipoUsuario tipoUsuario, bool flag)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Quadra = quadra;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Senha = senha;
            DataNascimento = dataNascimento;
            CPF = cpf;
            TipoUsuario = tipoUsuario;
            Flag = flag;
        }

        public Usuario() { }
    }


}