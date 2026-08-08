using System;
using Domain.ValueObjects;

namespace Application.Features.Users.Instructor.Constructor
{
    public class ConstructorInstructor
    {
        public Guid IdInstrutor { get; private set; }
        public Guid IdUsuario { get; private set; }
        public CREF CREF { get; private set; }

        public ConstructorInstructor(Guid idUsuario, string cref)
        {
            IdInstrutor = Guid.NewGuid();
            IdUsuario = idUsuario;
            CREF = new CREF(cref);
        }
    }
}