using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Features.Users.Instructor.Entities
{
    public class Instructor
    {
        public Guid IdInstrutor { get; private set; }
        public Guid IdAcademia { get; private set; }
        public Guid IdUsuario { get; private set; }
        public CREF CREF { get; private set; }

        public Instructor(Guid idInstrutor, Guid idUsuario, CREF cref)
        {
            IdInstrutor = idInstrutor;
            IdUsuario = idUsuario;
            CREF = cref;
        }

        public Instructor() { }
    }


}