using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAluno
    {
        List<Usuario> ListarTodosALunos(Guid idacademia);
        dynamic VincularInstrutor(Guid idaluno, Guid idprofessor);
        dynamic AtualizarObjetivo(Guid idaluno, string novoobjetivo);
        dynamic ConsultarFrequencia(Guid idaluno);
        dynamic ConsultarFicha(Guid idaluno);

    }
}
