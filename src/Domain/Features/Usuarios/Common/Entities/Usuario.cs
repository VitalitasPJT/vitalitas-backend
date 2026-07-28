using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Usuarios.Common.Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; private set; }
        public Guid IdAcademia { get; private set; }
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public CPF CPF { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }
        public bool Ativo { get; private set; }
        public bool Flag { get; private set; }
        public string Quadra { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        
        public Usuario(Guid idUsuario, Guid idAcademia, string nome, Email email, string senha, DateOnly dataNascimento, CPF cpf, TipoUsuario tipoUsuario, bool ativo, bool flag, string quadra, string rua, string bairro, string cidade, string estado, string cep)
        {
            IdUsuario = idUsuario;
            IdAcademia = idAcademia;
            Nome = Nome.Create(nome);
            Email = email;
            Senha = senha;
            DataNascimento = dataNascimento;
            CPF = cpf;
            TipoUsuario = tipoUsuario;
            Ativo = ativo;
            Flag = flag;
            Quadra = quadra;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }

        public Usuario(){}
    }
}