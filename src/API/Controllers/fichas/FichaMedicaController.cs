using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Application.DTOs.Request.FichaMedicaRQ;
using static Application.DTOs.Response.FichaMedicaRP;
using static DTOs.Constructor.Constructor;

namespace API.Controllers
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