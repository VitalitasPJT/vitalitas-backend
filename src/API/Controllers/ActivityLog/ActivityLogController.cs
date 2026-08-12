using API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.ActivityLog.Response.ActivityLogRP;
using Application.Features.ActivityLog.Interfaces;

namespace API.Controllers.ActivityLog
{
    [ApiController]
    [Route("log-atividades")]
    [Authorize(Policy = AuthorizationPolicies.PodeVerLogs)]
    public class ActivityLogController : ControllerBase
    {
        private readonly IActivityLogUseCase _activityLogUseCase;

        public ActivityLogController(IActivityLogUseCase activityLogUseCase)
        {
            _activityLogUseCase = activityLogUseCase;
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<ActivityLogResponse> BuscarLogsPorAcademia()
        {
            try
            {
                var idAcademia = User.FindFirst("IdAcademia")?.Value;
                if (string.IsNullOrEmpty(idAcademia) || !Guid.TryParse(idAcademia, out Guid parsedIdAcademia))
                {
                    return Unauthorized(new { message = "Token JWT inválido ou não contém o IdAcademia." });
                }
                var response = _activityLogUseCase.BuscarLogsPorAcademia(parsedIdAcademia);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }
    }
}