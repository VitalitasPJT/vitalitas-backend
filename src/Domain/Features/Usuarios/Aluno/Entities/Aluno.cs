using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Features.Usuarios.Aluno.Entities
{
    public class Aluno
    {
        public Guid IdAluno { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Guid IdInstrutor { get; private set; }
        public Guid IdContrato { get; private set; }
        public string Objetivo { get; private set; } = string.Empty;

        public Aluno(Guid idAluno, Guid idUsuario, Guid idInstrutor, Guid idContrato, string objetivo)
        {
            IdAluno = idAluno;
            IdUsuario = idUsuario;
            IdInstrutor = idInstrutor;
            IdContrato = idContrato;
            Objetivo = objetivo;
        }

        public Aluno() { }
    }
}