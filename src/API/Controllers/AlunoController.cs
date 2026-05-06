using Microsoft.AspNetCore.Mvc;
using Vitalitas.Backend.API.Services.JwtService;
using static Application.DTOs.AlunoRQ;
using static Application.DTOs.AlunoRP; 
using Application.Interfaces;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("vitalitas/aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoUseCase _alunoUseCase;

        public AlunoController(IAlunoUseCase alunoUseCase)
        {
            _alunoUseCase = alunoUseCase;
        }

        [HttpPost]
        public ActionResult<CriarAlunoResponse> CriarAluno([FromBody] CriarAlunoRequest aluno)
        {
            try
            {
                var response = _alunoUseCase.CriarAluno(aluno.IdInstrutor, aluno.IdUsuario, aluno.IdContrato, aluno.IdAcademia, aluno.Objetivo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("listar-alunos")]
        public ActionResult<ListarAlunosResponse> ListarAlunos([FromQuery] ListarAlunosRequest academia)
        {
            try
            {
                var response = _alunoUseCase.ListarAlunos(academia.IdAcademia);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpGet("listar-aluno")]
        public ActionResult<ListarAlunoResponse> ListarAluno([FromQuery] ListarAlunoRequest aluno)
        {
            try
            {
                if ( aluno.IdAluno == null)    
                {
                    return BadRequest(new { message = "ID do aluno inválido. Certifique-se de fornecer um GUID válido." });
                }
                var response = _alunoUseCase.ListarAluno(aluno.IdAluno);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPut("vincular-instrutor")]
        public ActionResult<VincularInstrutorResponse> VincularInstrutor([FromBody] VincularInstrutorRequest request)
        {
            try
            {
                var response = _alunoUseCase.VincularInstrutor(request.IdAluno, request.IdInstrutor);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Atributo inválido", detalhe = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpPut("atualizar-objetivo")]
        public ActionResult<AtualizarObjetivoResponse> AtualizarObjetivo([FromRoute] AtualizarObjetivoRequest objetivo)
        {
            try
            {
                var response = _alunoUseCase.AtualizarObjetivo(objetivo.IdAluno, objetivo.NovoObjetivo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }
    }
}