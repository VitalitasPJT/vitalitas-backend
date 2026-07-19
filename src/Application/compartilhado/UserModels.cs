using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace DTOs.Constructor
{
    public class Constructor
    {
        public class ConstructorUsuario
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

            public ConstructorUsuario(Guid idAcademia, string nome, Email email, string senha, DateOnly dataNascimento, CPF cpf, TipoUsuario tipoUsuario, string quadra, string rua, string bairro, string cidade, string estado, string cep)
            {
                IdUsuario = Guid.NewGuid();
                IdAcademia = idAcademia;
                Nome = Nome.Create(nome);
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

        public class ConstructorAluno
        {
            public Guid IdAluno { get; private set; }
            public Guid IdUsuario { get; private set; }
            public Guid IdInstrutor { get; private set; }
            public Guid IdContrato { get; private set; }
            public string Objetivo { get; private set; }

            public ConstructorAluno(Guid idUsuario, Guid idInstrutor, Guid idContrato, string objetivo)
            {
                IdAluno = Guid.NewGuid();
                IdUsuario = idUsuario;
                IdInstrutor = idInstrutor;
                IdContrato = idContrato;
                Objetivo = objetivo;
            }
        }  

        public class ConstructorInstrutor
        {
            public Guid IdInstrutor { get; private set; }
            public Guid IdUsuario { get; private set; }
            public CREF CREF { get; private set; }

            public ConstructorInstrutor(Guid idUsuario, string cref)
            {
                IdInstrutor = Guid.NewGuid();
                IdUsuario = idUsuario;
                CREF = new CREF(cref);
            }
        }

        public class ConstructorFuncionario
        {
            public Guid IdUsuario { get; private set; }
            public Cargo Cargo { get; private set; }

            public ConstructorFuncionario(Guid idUsuario, Cargo cargo)
            {
                IdUsuario = idUsuario;
                Cargo = cargo;
            }
        }

        public class ConstructorGestor
        {
            public Guid IdUsuario { get; private set; }

            public ConstructorGestor(Guid idUsuario)
            {
                IdUsuario = idUsuario;
            }
        }

        public class ConstructorFichaMedica
        {
            public Guid IdFicha { get; private set; }
            public Guid IdAluno { get; private set; }
            public string Alergia { get; private set; }
            public string Restricao { get; private set; }
            public string Lesao { get; private set; }
            public string Cirurgia { get; private set; }
            public string ProblemaSaude { get; private set; }
            public string UsoMedicamento { get; private set; }

            public ConstructorFichaMedica(Guid idAluno, string alergia, string restricao, string lesao, string cirurgia, string problemaSaude, string usoMedicamento)
            {
                IdFicha = Guid.NewGuid();
                IdAluno = idAluno;
                Alergia = alergia;
                Restricao = restricao;
                Lesao = lesao;
                Cirurgia = cirurgia;
                ProblemaSaude = problemaSaude;
                UsoMedicamento = usoMedicamento;
            }
        }
    }
}