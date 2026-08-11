using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Authorization;
using API.Services;
using static Application.Features.Users.Member.Request.MemberRQ;
using static Application.Features.Users.Member.Response.MemberRP;
using Application.Features.Users.Manager.Interfaces;
using System;
using System.Collections.Generic;
using Application.Features.Users.Manager.Response;
using static Application.Features.Users.Manager.Request.ManagerRQ;
using Application.Features.Users.Common.Constructor;
using Application.Features.Users.Member.Constructor;
using Application.Features.Users.Instructor.Constructor;
using Application.Features.Users.Employee.Constructor;
using Application.Features.Users.Manager.Constructor;
using Domain.Enums;
using Domain.ValueObjects;

namespace API.Controllers.Users
{
    [ApiController]
    [Route("gestor")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerUseCase _gestorUseCase;
        private readonly Dictionary<Type, (UserType TipoUsuario, Func<ConstructorUser, CreatePerfilRequest, CreateUserWithProfileResponse> Criar)> _criadoresDePerfil;

        public ManagerController(IManagerUseCase gestorUseCase)
        {
            _gestorUseCase = gestorUseCase;

            _criadoresDePerfil = new Dictionary<Type, (UserType, Func<ConstructorUser, CreatePerfilRequest, CreateUserWithProfileResponse>)>
            {
                [typeof(CreateMemberProfileRequest)] = (UserType.Aluno, (usuario, perfil) =>
                {
                    var p = (CreateMemberProfileRequest)perfil;
                    return _gestorUseCase.CriarUsuarioAluno(usuario, new ConstructorMember(usuario.IdUsuario, p.IdInstrutor, p.IdContrato, p.Objetivo));
                }),
                [typeof(CreateInstructorProfileRequest)] = (UserType.Instrutor, (usuario, perfil) =>
                {
                    var p = (CreateInstructorProfileRequest)perfil;
                    return _gestorUseCase.CriarUsuarioInstrutor(usuario, new ConstructorInstructor(usuario.IdUsuario, p.CREF));
                }),
                [typeof(CreateEmployeeProfileRequest)] = (UserType.Administrador, (usuario, perfil) =>
                {
                    var p = (CreateEmployeeProfileRequest)perfil;
                    return _gestorUseCase.CriarUsuarioAdministrador(usuario, new ConstructorEmployee(usuario.IdUsuario, p.Cargo));
                }),
            };
        }

        [HttpPost("criar-usuario")]
        [Authorize(Policy = AuthorizationPolicies.PodeGerenciarUsuarios)]
        [ApiExplorerSettings(GroupName = "Gestor")]
        public ActionResult<CreateUserWithProfileResponse> CriarUsuario([FromBody] CreateUserWithProfileRequest request)
        {
            if (!_criadoresDePerfil.TryGetValue(request.Perfil.GetType(), out var criador))
                return BadRequest(new { message = "Tipo de perfil não reconhecido." });

            // Matriz de permissões: só Gestor cria Administrador/Instrutor.
            // Administrador só pode criar Aluno por aqui (Criar Alunos: Gestor+Admin).
            if (criador.TipoUsuario != UserType.Aluno && User.FindFirst("Role")?.Value != "Gestor")
                return Forbid();

            try
            {
                var u = request.Usuario;
                var constructorUsuario = new ConstructorUser(u.IdAcademia, u.Nome, new Email(u.Email), u.Senha, u.DataNascimento, new CPF(u.Cpf), criador.TipoUsuario, u.Quadra, u.Rua, u.Bairro, u.Cidade, u.Estado, u.Cep);

                var response = criador.Criar(constructorUsuario, request.Perfil);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", detalhe = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        [HttpGet("listar-alunos/{idAcademia}")]
        [Authorize(Policy = AuthorizationPolicies.PodeGerenciarAlunos)]
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
        [Authorize(Policy = AuthorizationPolicies.PodeGerenciarUsuarios)]
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
        [Authorize(Policy = AuthorizationPolicies.PodeGerenciarUsuarios)]
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