using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Usuarios.Instrutor.Interfaces
{
    public interface IInstrutorRepository
    {
        List<Domain.Features.Usuarios.Instrutor.Entities.Instrutor> ListarInstrutores(Guid idAcademia);
        List<dynamic> ListarAlunos(Guid idInstrutor);
        dynamic TrocarSenha(Guid idUsuario, string novaSenha);
    }
}
