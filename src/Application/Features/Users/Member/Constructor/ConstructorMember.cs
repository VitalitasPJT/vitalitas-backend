using System;

namespace Application.Features.Users.Member.Constructor
{
    public class ConstructorMember
    {
        public Guid IdAluno { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Guid IdInstrutor { get; private set; }
        public Guid IdContrato { get; private set; }
        public string Objetivo { get; private set; }

        public ConstructorMember(Guid idUsuario, Guid idInstrutor, Guid idContrato, string objetivo)
        {
            IdAluno = Guid.NewGuid();
            IdUsuario = idUsuario;
            IdInstrutor = idInstrutor;
            IdContrato = idContrato;
            Objetivo = objetivo;
        }
    }
}