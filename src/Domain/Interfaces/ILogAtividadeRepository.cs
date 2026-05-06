using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILogAtividadeRepository
    {
        Task RegistrarAtividadeAsync(Guid idUsuario, DateTime dataHora, AcaoLog acao, string dispositivoLogado, string localizacao);
        Task<IEnumerable<LogAtividade>> ObterLogsPorUsuarioAsync(Guid idUsuario);
        Task<IEnumerable<LogAtividade>> ObterLogsPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<LogAtividade>> ObterLogsPorAcaoAsync(AcaoLog acao);
        Task<int> ContarAtividadesPorUsuarioAsync(Guid idUsuario);
        Task<LogAtividade> ObterUltimaAtividadePorUsuarioAsync(Guid idUsuario);
    }
}
