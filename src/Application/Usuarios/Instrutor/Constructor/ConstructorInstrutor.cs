using System;
using Domain.ValueObjects;

namespace Application.Usuarios.Instrutor.Constructor
{
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
}