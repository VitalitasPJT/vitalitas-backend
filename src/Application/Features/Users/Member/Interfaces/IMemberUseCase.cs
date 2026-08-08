using Application.Shared;
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.Features.Users.Member.Response.MemberRP;
using static Application.Features.Users.Common.Response.UserRS;

namespace Application.Features.Users.Member.Interfaces
{
    public interface IMemberUseCase
    {
        ListMemberResponse ListarAluno(Guid idaluno);

        LinkInstructorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor);

        UpdateGoalResponse AtualizarObjetivo(Guid idusuario, string objetivo);
        HttpStatus TrocarSenha(Guid idusuario, string novaSenha);
        Guid? ObterIdUsuarioPorAluno(Guid idAluno);
    }
}