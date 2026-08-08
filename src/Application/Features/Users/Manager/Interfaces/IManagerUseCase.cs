using Application.Shared;
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.Features.Users.Member.Response.MemberRP;
using static Application.Features.Users.Manager.Request.ManagerRQ;
using Application.Features.Users.Manager.Response;
using static Application.Features.Users.Common.Response.UserRS;
using Application.Features.Users.Common.Constructor;
using Application.Features.Users.Member.Constructor;
using Application.Features.Users.Instructor.Constructor;
using Application.Features.Users.Employee.Constructor;
using Application.Features.Users.Manager.Constructor;

namespace Application.Features.Users.Manager.Interfaces
{
    public interface IManagerUseCase
    {
        CreateUserResponse CriarUsuario(ConstructorUser usuario);
        CreateMemberResponse CriarAluno(ConstructorMember aluno);
        CreateInstructorResponse CriarInstrutor(ConstructorInstructor instrutor);
        CreateEmployeeResponse CriarFuncionario(ConstructorEmployee funcionario);
        CreateManagerResponse CriarGestor(ConstructorManager gestor);
        ListMembersResponse ListarAlunos(Guid idAcademia);
        ListUsersResponse ListarUsuarios(Guid idAcademia);
        GetManagerResponse ObterGestor(Guid idUsuario);
        ListUserResponse ListarUsuario(Guid idUsuario);
    }
}