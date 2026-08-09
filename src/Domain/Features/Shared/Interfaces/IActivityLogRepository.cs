using Domain.Features.Shared.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Interfaces
{
    public interface IActivityLogRepository
    {
        Task RegistrarAtividadeAsync(Guid idUsuario, DateTime dataHora, LogAction acao, string dispositivoLogado, string localizacao);
        Task<IEnumerable<ActivityLog>> ObterLogsPorUsuarioAsync(Guid idUsuario);
        Task<IEnumerable<ActivityLog>> ObterLogsPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<ActivityLog>> ObterLogsPorAcaoAsync(LogAction acao);
        Task<int> ContarAtividadesPorUsuarioAsync(Guid idUsuario);
        Task<ActivityLog> ObterUltimaAtividadePorUsuarioAsync(Guid idUsuario);
    }
}
