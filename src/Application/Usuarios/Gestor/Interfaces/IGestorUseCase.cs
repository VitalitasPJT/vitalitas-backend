using Application.Compartilhado;
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.Usuarios.Aluno.Response.AlunoRP;
using static Application.Usuarios.Gestor.Request.GestorRQ;
using Application.Usuarios.Gestor.Response;
using static Application.Usuarios.Common.Response.UsuarioRS;
using Application.Usuarios.Common.Constructor;
using Application.Usuarios.Aluno.Constructor;
using Application.Usuarios.Instrutor.Constructor;
using Application.Usuarios.Funcionario.Constructor;
using Application.Usuarios.Gestor.Constructor;

namespace Application.Usuarios.Gestor.Interfaces
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