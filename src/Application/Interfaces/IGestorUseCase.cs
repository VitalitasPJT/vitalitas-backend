using Application.DTOs;
using Domain.Entities; 
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.DTOs.AlunoRP;
using static Application.DTOs.Request.GestorRQ;
using static Application.DTOs.Response.GertorRP;
using static Application.DTOs.UsuarioRS;
using static DTOs.Constructor.Constructor;

namespace Application.Interfaces
{
    public interface IGestorUseCase
    {
        CriarUsuarioResponse CriarUsuario(ConstructorUsuario usuario);
        CriarAlunoResponse CriarAluno(ConstructorAluno aluno);
        CriarInstrutorResponse CriarInstrutor(ConstructorInstrutor instrutor);
        CriarFuncionarioResponse CriarFuncionario(ConstructorFuncionario funcionario);
        CriarGestorResponse CriarGestor(ConstructorGestor gestor);
        ListarAlunosResponse ListarAlunos(Guid idAcademia);
        ListarUsuariosResponse ListarUsuarios(Guid idAcademia);
        ObterGestorResponse ObterGestor(Guid idUsuario);
        ListarUsuarioResponse ListarUsuario(Guid idUsuario);
    }
}