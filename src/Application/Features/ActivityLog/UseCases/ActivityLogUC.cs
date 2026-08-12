using Application.Shared;
using Domain.Enums;
using static Application.Features.ActivityLog.Response.ActivityLogRP;
using Application.Features.ActivityLog.Interfaces;
using Domain.Features.Shared.Interfaces;

namespace Application.Features.ActivityLog.UseCases
{
    public class ActivityLogUC : IActivityLogUseCase
    {
        private readonly IActivityLogRepository _activityLogRepository;
        public ActivityLogUC(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public ActivityLogResponse BuscarLogsPorAcademia(Guid idAcademia)
        {
            var result = _activityLogRepository.ObterLogsPorAcademia(idAcademia);

            var logs = new List<ActivityLogItem>();
            for (int i = 0; i < result.Count; i++)
            {
                logs.Add(new ActivityLogItem
                {
                    IdLog = result[i].idLog,
                    NomeUsuario = result[i].nome,
                    EmailUsuario = result[i].email,
                    DispositivoLogado = result[i].dispositivoLogado,
                    Acao = (LogAction)result[i].acao,
                    DataHora = result[i].dataHora
                });
            }

            if (logs.Count == 0)
            {
                var statusVazio = new HttpStatus("Nenhum log de atividade encontrado para a academia especificada.", 404, false);
                return new ActivityLogResponse(new List<ActivityLogItem>(), statusVazio);
            }

            var status = new HttpStatus("Logs de atividade encontrados com sucesso.", 200, true);
            return new ActivityLogResponse(logs, status);
        }
    }
}
