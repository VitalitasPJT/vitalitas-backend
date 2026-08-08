using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Features.Users.Member.Entities;

namespace Domain.Features.Users.Member.Interfaces
{
    public interface IXpHistoryRepository
    {
        dynamic RegistrarXp(Guid idAluno, int xpGanho, string motivo, DateTime data);
        XpHistory ObterXp(Guid idXp);
        List<XpHistory> ListarXpPorData(DateTime data, Guid idAluno);
        List<XpHistory> ListarXpPorPeriodo(DateTime dataInicio, DateTime dataFim, Guid idAluno);
        List<XpHistory> ListarTodos(Guid idAluno);
        double CalcularTotalXp(Guid idAluno, DateTime dataInicio, DateTime dataFim);
        double CalcularMediaXp(Guid idAluno, DateTime dataInicio, DateTime dataFim);
        dynamic ExcluirXp(Guid idXp);
        dynamic AtualizarXp(Guid idXp, int novoXp);
    }
}