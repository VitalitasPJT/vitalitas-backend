using System;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Features.Users.Common.Constructor
{
    public class ConstructorUser
    {
        public Guid IdUsuario { get; private set; }
        public Guid IdAcademia { get; private set; }
        public Name Nome { get; private set; }
        public Email Email { get; private set; }
        public string Senha { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public CPF CPF { get; private set; }
        public UserType TipoUsuario { get; private set; }
        public bool Ativo { get; private set; }
        public bool Flag { get; private set; }
        public string Quadra { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }

        public ConstructorUser(Guid idAcademia, string nome, Email email, string senha, DateOnly dataNascimento, CPF cpf, UserType tipoUsuario, string quadra, string rua, string bairro, string cidade, string estado, string cep)
        {
            IdUsuario = Guid.NewGuid();
            IdAcademia = idAcademia;
            Nome = Name.Create(nome);
            Email = email;
            Senha = senha;
            DataNascimento = dataNascimento;
            CPF = cpf;
            TipoUsuario = tipoUsuario;
            Ativo = true;
            Flag = true;
            Quadra = quadra;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
        }
    }
}