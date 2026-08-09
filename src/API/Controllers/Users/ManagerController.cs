using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using static Application.Features.Users.Member.Request.MemberRQ;
using static Application.Features.Users.Member.Response.MemberRP;
using Application.Features.Users.Manager.Interfaces;
using System;
using Application.Features.Users.Manager.Response;
using static Application.Features.Users.Manager.Request.ManagerRQ;
using Application.Features.Users.Common.Constructor;
using Application.Features.Users.Member.Constructor;
using Application.Features.Users.Instructor.Constructor;
using Application.Features.Users.Employee.Constructor;
using Application.Features.Users.Manager.Constructor;
using Domain.ValueObjects;

namespace API.Controllers.Users
{
    [ApiController]
    [Route("gestor")]
    [Authorize(Roles = "Gestor,Administrador")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerUseCase _gestorUseCase;

        public ManagerController(IManagerUseCase gestorUseCase)
        {
            _gestorUseCase = gestorUseCase;
        }

        [HttpPost("criar-usuario")]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CreateUserResponse> CriarUsuario([FromBody] CreateUserRequest usuario)
        {
            try
            {
                var constructorUsuario = new ConstructorUser(usuario.IdAcademia, usuario.Nome, new Email(usuario.Email), usuario.Senha, usuario.DataNascimento, new CPF(usuario.Cpf), usuario.TipoUsuario, usuario.Quadra, usuario.Rua, usuario.Bairro, usuario.Cidade, usuario.Estado, usuario.Cep);
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
        public ActionResult<CreateMemberResponse> CriarAluno([FromBody] CreateMemberRequest aluno)
        {
            try
            {
                var constructorAluno = new ConstructorMember(aluno.IdUsuario, aluno.IdInstrutor, aluno.IdContrato, aluno.Objetivo);
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
        public ActionResult<CreateInstructorResponse> CriarInstrutor([FromBody] CreateInstructorRequest instrutor)
        {
            try
            {
                var constructorInstrutor = new ConstructorInstructor(instrutor.IdUsuario, instrutor.CREF);
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
        public ActionResult<ListMembersResponse> ListarAlunos([FromRoute] Guid idAcademia)
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
        public ActionResult<ListUsersResponse> ListarUsuarios([FromRoute] Guid idAcademia)
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
        public ActionResult<ListUsersResponse> ListarUsuario([FromRoute] Guid idUsuario)
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