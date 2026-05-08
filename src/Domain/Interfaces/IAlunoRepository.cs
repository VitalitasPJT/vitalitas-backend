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
        dynamic ListarAluno(Guid idAluno);
        bool VincularInstrutor(Guid idAluno, Guid idInstrutor);
        bool AtualizarObjetivo(Guid idAluno, string novoObjetivo);
        bool TrocarSenha(Guid idUsuario, string novaSenha);
    }
}
