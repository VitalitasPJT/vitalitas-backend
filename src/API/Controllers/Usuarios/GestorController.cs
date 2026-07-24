using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using static Application.Usuarios.Aluno.Request.AlunoRQ;
using static Application.Usuarios.Aluno.Response.AlunoRP;
using Application.Usuarios.Gestor.Interfaces;
using System;
using Application.Usuarios.Gestor.Response;
using static Application.Usuarios.Gestor.Request.GestorRQ;
using Application.Usuarios.Common.Constructor;
using Application.Usuarios.Aluno.Constructor;
using Application.Usuarios.Instrutor.Constructor;
using Application.Usuarios.Funcionario.Constructor;
using Application.Usuarios.Gestor.Constructor;
using Domain.ValueObjects;

namespace API.Controllers.Usuarios
{
    [ApiController]
    [Route("gestor")]
    [Authorize(Roles = "Gestor,Administrador")]
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

        [HttpPost("criar-aluno")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CriarAlunoResponse> CriarAluno([FromBody] CriarAlunoRequest aluno)
        {
            try
            {
                var constructorAluno = new ConstructorAluno(aluno.IdUsuario, aluno.IdInstrutor, aluno.IdContrato, aluno.Objetivo);
                var response = _gestorUseCase.CriarAluno(constructorAluno);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpPost("criar-instrutor")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CriarInstrutorResponse> CriarInstrutor([FromBody] CriarInstrutorRequest instrutor)
        {
            try
            {
                var constructorInstrutor = new ConstructorInstrutor(instrutor.IdUsuario, instrutor.CREF);
                var response = _gestorUseCase.CriarInstrutor(constructorInstrutor);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("listar-alunos/{idAcademia}")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<ListarAlunosResponse> ListarAlunos([FromRoute] Guid idAcademia)
        {
            try
            {
                var response = _gestorUseCase.ListarAlunos(idAcademia);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }

        [HttpGet("listar-usuarios/{idAcademia}")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<ListarUsuariosResponse> ListarUsuarios([FromRoute] Guid idAcademia)
        {
            try
            {
                var response = _gestorUseCase.ListarUsuarios(idAcademia);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }


        [HttpGet("listar-usuario/{idUsuario}")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<ListarUsuariosResponse> ListarUsuario([FromRoute] Guid idUsuario)
        {
            try
            {
                var response = _gestorUseCase.ListarUsuario(idUsuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message });
            }
        }
    }
}