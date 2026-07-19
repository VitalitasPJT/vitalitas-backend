using System;

namespace Application.Usuarios.Aluno.DTO
{
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
}