using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IXpHistoricoRepository
    {
        dynamic RegistrarXp(Guid idAluno, int xpGanho, string motivo, DateTime data);
        XpHistorico ObterXp(Guid idXp);
        List<XpHistorico> ListarXpPorData(DateTime data, Guid idAluno);
        List<XpHistorico> ListarXpPorPeriodo(DateTime dataInicio, DateTime dataFim, Guid idAluno);
        List<XpHistorico> ListarTodos(Guid idAluno);
        double CalcularTotalXp(Guid idAluno, DateTime dataInicio, DateTime dataFim);
        double CalcularMediaXp(Guid idAluno, DateTime dataInicio, DateTime dataFim);
        dynamic ExcluirXp(Guid idXp);
        dynamic AtualizarXp(Guid idXp, int novoXp);
    }
}