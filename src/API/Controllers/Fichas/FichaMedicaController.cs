using Application.Fichas.FichaMedica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Fichas.FichaMedica.Request.FichaMedicaRQ;
using static Application.Fichas.FichaMedica.Response.FichaMedicaRP;
using Application.Fichas.FichaMedica.Constructor;

namespace API.Controllers.Fichas
{
    [ApiController]
    [Route("ficha-medica")]
    [Authorize(Roles = "Gestor,Administrador")]
    public class FichaMedicaController : Controller
    {
        private readonly IFichaMedicaUseCase _fichaMedicaUseCase;

        public FichaMedicaController(IFichaMedicaUseCase fichaMedicaUseCase)
        {
            _fichaMedicaUseCase = fichaMedicaUseCase;
        }
     
        [HttpPost]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CriarFichaMedicaResponse> CriarFichaMedica([FromBody] CriarFichaMedicaRequest fichaMedica)
        {
            try
            {
                var constructorFichaMedica = new ConstructorFichaMedica(fichaMedica.IdAluno, fichaMedica.Alergia, fichaMedica.Restricao, fichaMedica.Lesao, fichaMedica.Cirurgia, fichaMedica.ProblemaSaude, fichaMedica.UsoMedicamento);
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