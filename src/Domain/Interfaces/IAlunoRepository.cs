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
        Guid CriarAluno(Guid idInstrutor, Guid idUsuario, int idContrato, Guid idAcademia, string objetivo);
        List<dynamic> ListarALunos(Guid idacademia);
        dynamic ListarAluno(Guid idacademia);
        dynamic VincularInstrutor(Guid idaluno, Guid idprofessor);
        dynamic AtualizarObjetivo(Guid idaluno, string novoobjetivo);

    }
}
