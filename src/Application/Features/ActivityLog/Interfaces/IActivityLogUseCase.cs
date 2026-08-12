using static Application.Features.ActivityLog.Response.ActivityLogRP;

namespace Application.Features.ActivityLog.Interfaces
{
    public interface IActivityLogUseCase
    {
        ActivityLogResponse BuscarLogsPorAcademia(Guid idAcademia);
    }
}
