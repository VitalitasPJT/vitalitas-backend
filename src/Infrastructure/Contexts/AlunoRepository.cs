using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class AlunoRepository : IAluno
    {
        public dynamic AtualizarObjetivo(Guid idaluno, string novoobjetivo)
        {
            throw new NotImplementedException();
        }

        public dynamic ConsultarFicha(Guid idaluno)
        {
            throw new NotImplementedException();
        }

        public dynamic ConsultarFrequencia(Guid idaluno)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAllUsuarios(Guid idacademia)
        {
            throw new NotImplementedException();
        }

        public dynamic VincularInstrutor(Guid idaluno, Guid idprofessor)
        {
            throw new NotImplementedException();
        }
    }
}
