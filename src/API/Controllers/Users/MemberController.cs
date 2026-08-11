using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Authorization;
using API.Services;
using static Application.Features.Users.Member.Request.MemberRQ;
using static Application.Features.Users.Member.Response.MemberRP;
using Application.Features.Users.Member.Interfaces;
using System;

namespace API.Controllers.Users
{
    [ApiController]
    [Route("aluno")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberUseCase _alunoUseCase;

        public MemberController(IMemberUseCase alunoUseCase)
        {
            _alunoUseCase = alunoUseCase;
        }

        [HttpGet("listar-aluno")]
        [Authorize]
        public ActionResult<ListMemberResponse> ListarAluno([FromQuery] ListMemberRequest aluno)
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
        [Authorize(Policy = AuthorizationPolicies.PodeTrocarSenha)]
        [ApiExplorerSettings(GroupName = "Aluno")]
        public ActionResult<ChangePasswordResponse> TrocarSenha([FromBody] ChangePasswordRequest reset)
        {
            var jwtId = User.FindFirst("IdUsuario")?.Value;
            if (reset.IdUsuario.ToString() != jwtId)
                return Forbid();

            try
            {
                var response = _alunoUseCase.TrocarSenha(reset.IdUsuario, reset.NovaSenha);
                var trocarSenhaResponse = new ChangePasswordResponse(response);
                return Ok(trocarSenhaResponse);
            }
            catch (Exception ex)
            {
                var status = new Application.Shared.HttpStatus(ex.Message, 400, false);
                return BadRequest(new ChangePasswordResponse(status));
            }
        }

        [HttpPut("vincular-instrutor")]
        [Authorize(Policy = AuthorizationPolicies.PodeGerenciarAlunos)]
        [ApiExplorerSettings(GroupName = "Aluno")]
        public ActionResult<LinkInstructorResponse> VincularInstrutor([FromBody] LinkInstructorRequest request)
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
        [Authorize(Policy = AuthorizationPolicies.PodeAtualizarObjetivoAluno)]
        [ApiExplorerSettings(GroupName = "Aluno")]
        public ActionResult<UpdateGoalResponse> AtualizarObjetivo([FromBody] UpdateGoalRequest objetivo)
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