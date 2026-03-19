using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Aluno
    {
        public Guid IdAluno { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Guid IdContrato { get; private set; }
        public Guid IdAcademia { get; private set; }
        public List<Guid> IdXp { get; private set; }
        public string Objetivo { get; private set; }
    }
}