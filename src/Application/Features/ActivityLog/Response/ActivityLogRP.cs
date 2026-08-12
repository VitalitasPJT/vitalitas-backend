using Application.Shared;
using Domain.Enums;

namespace Application.Features.ActivityLog.Response
{
    public class ActivityLogRP
    {
        public class ActivityLogItem
        {
            public Guid IdLog { get; set; }
            public required string NomeUsuario { get; set; }
            public required string EmailUsuario { get; set; }
            public string? DispositivoLogado { get; set; }
            public LogAction Acao { get; set; }
            public DateTime DataHora { get; set; }
        }

        public class ActivityLogResponse
        {
            public ActivityLogResponse(List<ActivityLogItem> logs, HttpStatus status)
            {
                Logs = logs;
                Status = status;
            }

            public List<ActivityLogItem> Logs { get; set; }
            public HttpStatus Status { get; set; }
        }
    }
}
