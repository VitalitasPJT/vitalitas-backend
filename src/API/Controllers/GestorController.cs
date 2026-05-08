using Microsoft.AspNetCore.Mvc;
using Vitalitas.Backend.API.Services.JwtService;
using static Application.DTOs.AlunoRQ;
using static Application.DTOs.AlunoRP;
using Application.Interfaces;
using System;
using static Application.DTOs.Response.GertorRP;
using static Application.DTOs.Request.GestorRQ;
using static DTOs.Constructor.Constructor;
using Domain.ValueObjects;

namespace API.Controllers
{
    [ApiController]
    [Route("gestor")]
    public class GestorController : ControllerBase
    {
        private readonly IGestorUseCase _gestorUseCase;

        public GestorController(IGestorUseCase gestorUseCase)
        {
            _gestorUseCase = gestorUseCase;
        }

        [HttpPost("criar-usuario")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CriarUsuarioResponse> CriarUsuario([FromBody] CriarUsuarioRequest usuario)
        {
            try
            {
                var constructorUsuario = new ConstructorUsuario(usuario.IdAcademia, usuario.Nome, new Email(usuario.Email), usuario.Senha, usuario.DataNascimento, new CPF(usuario.Cpf), usuario.TipoUsuario, usuario.Quadra, usuario.Rua, usuario.Bairro, usuario.Cidade, usuario.Estado, usuario.Cep);
                var response = _gestorUseCase.CriarUsuario(constructorUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        /*[HttpPost]
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
        }*/
    }
}