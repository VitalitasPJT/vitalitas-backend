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
        dynamic ListarAluno(Guid idAcademia, Guid idUsuario);
        dynamic VincularInstrutor(Guid idAluno, Guid idInstrutor);
        dynamic AtualizarObjetivo(Guid idAluno, string novoObjetivo);
        dynamic TrocarSenha(Guid idUsuario, string novaSenha);
    }
}
