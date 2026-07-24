using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Services;
using static Application.Usuarios.Aluno.Request.AlunoRQ;
using static Application.Usuarios.Aluno.Response.AlunoRP;
using Application.Usuarios.Aluno.Interfaces;
using System;

namespace API.Controllers.Usuarios
{
    [ApiController]
    [Route("aluno")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoUseCase _alunoUseCase;

        public AlunoController(IAlunoUseCase alunoUseCase)
        {
            _alunoUseCase = alunoUseCase;
        }

        [HttpGet("listar-aluno")]
        [Authorize]
        public ActionResult<ListarAlunoResponse> ListarAluno([FromQuery] ListarAlunoRequest aluno)
        {
            if (aluno.IdAluno == Guid.Empty)
                return BadRequest(new { message = "ID do aluno inválido. Certifique-se de fornecer um GUID válido." });

            var role = User.FindFirst("Role")?.Value;
            if (role == "Aluno")
            {
                var jwtId = User.FindFirst("IdUsuario")?.Value;
                var donoId = _alunoUseCase.ObterIdUsuarioPorAluno(aluno.IdAluno);
                if (donoId?.ToString() != jwtId)
                    return Forbid();
            }

            try
            {
                var response = _alunoUseCase.ListarAluno(aluno.IdAluno);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPut("trocar-senha")]
        [Authorize(Roles = "Aluno")]
        [ApiExplorerSettings(GroupName = "Aluno")]
        public ActionResult<TrocarSenhaResponse> TrocarSenha([FromBody] TrocarSenhaRequest reset)
        {
            var jwtId = User.FindFirst("IdUsuario")?.Value;
            if (reset.IdUsuario.ToString() != jwtId)
                return Forbid();

            try
            {
                var response = _alunoUseCase.TrocarSenha(reset.IdUsuario, reset.NovaSenha);
                var trocarSenhaResponse = new TrocarSenhaResponse(response);
                return Ok(trocarSenhaResponse);
            }
            catch (Exception ex)
            {
                var status = new Application.Compartilhado.StatusHTTP(ex.Message, 400, false);
                return BadRequest(new TrocarSenhaResponse(status));
            }
        }

        [HttpPut("vincular-instrutor")]
        [Authorize(Roles = "Gestor,Administrador")]
        [ApiExplorerSettings(GroupName = "Aluno")]
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
        [Authorize(Roles = "Aluno,Gestor,Administrador")]
        [ApiExplorerSettings(GroupName = "Aluno")]
        public ActionResult<AtualizarObjetivoResponse> AtualizarObjetivo([FromBody] AtualizarObjetivoRequest objetivo)
        {
            var role = User.FindFirst("Role")?.Value;
            if (role == "Aluno")
            {
                var jwtId = User.FindFirst("IdUsuario")?.Value;
                var donoId = _alunoUseCase.ObterIdUsuarioPorAluno(objetivo.IdAluno);
                if (donoId?.ToString() != jwtId)
                    return Forbid();
            }

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