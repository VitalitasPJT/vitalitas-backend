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
        (List<Aluno>, List<Usuario>) ListarALunos(Guid idacademia);
        (Aluno, Usuario) ListarAluno(Guid idacademia, Guid idusuario);
        dynamic VincularInstrutor(Guid idaluno, Guid idprofessor);
        dynamic AtualizarObjetivo(Guid idaluno, string novoobjetivo);

    }
}
