using Domain.Enums;

namespace Domain.Features.Shared.Interfaces
{
    public interface IActivityLogRepository
    {
        Task RegistrarAtividadeAsync(Guid idUsuario, DateTime dataHora, LogAction acao, string dispositivoLogado, string localizacao);
        List<dynamic> ObterLogsPorAcademia(Guid idAcademia);
        //Task<IEnumerable<ActivityLog>> ObterLogsPorPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        //Task<IEnumerable<ActivityLog>> ObterLogsPorAcaoAsync(LogAction acao);
        //Task<int> ContarAtividadesPorUsuarioAsync(Guid idUsuario);
        //Task<ActivityLog> ObterUltimaAtividadePorUsuarioAsync(Guid idUsuario);
    }
}
