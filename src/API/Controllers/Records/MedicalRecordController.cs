using Application.Features.Records.MedicalRecord.Interfaces;
using API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.Records.MedicalRecord.Request.MedicalRecordRQ;
using static Application.Features.Records.MedicalRecord.Response.MedicalRecordRP;
using Application.Features.Records.MedicalRecord.Constructor;

namespace API.Controllers.Records
{
    [ApiController]
    [Route("ficha-medica")]
    // Matriz de permissões: "Ficha médica base — editar" é exclusiva do Instrutor
    // (Gestor/Administrador não editam ficha médica). Corrigido de
    // "Gestor,Administrador" para essa policy durante a migração para
    // AuthorizationPolicy nomeadas — ver ADR-0013.
    [Authorize(Policy = AuthorizationPolicies.PodeEditarFichaMedica)]
    public class MedicalRecordController : Controller
    {
        private readonly IMedicalRecordUseCase _fichaMedicaUseCase;

        public MedicalRecordController(IMedicalRecordUseCase fichaMedicaUseCase)
        {
            _fichaMedicaUseCase = fichaMedicaUseCase;
        }
     
        [HttpPost]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CreateMedicalRecordResponse> CriarFichaMedica([FromBody] CreateMedicalRecordRequest fichaMedica)
        {
            try
            {
                var constructorFichaMedica = new ConstructorMedicalRecord(fichaMedica.IdAluno, fichaMedica.Alergia, fichaMedica.Restricao, fichaMedica.Lesao, fichaMedica.Cirurgia, fichaMedica.ProblemaSaude, fichaMedica.UsoMedicamento);
                var response = _fichaMedicaUseCase.CriarFichaMedica(constructorFichaMedica);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }
    }
}