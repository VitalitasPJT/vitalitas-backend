using Application.Compartilhado;
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.Usuarios.Aluno.Response.AlunoRP;
using static Application.Usuarios.Common.Response.UsuarioRS;

namespace Application.Usuarios.Aluno.Interfaces
{
    public interface IAlunoUseCase
    {
        ListarAlunoResponse ListarAluno(Guid idaluno);

        VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor);

        AtualizarObjetivoResponse AtualizarObjetivo(Guid idusuario, string objetivo);
        StatusHTTP TrocarSenha(Guid idusuario, string novaSenha);
        Guid? ObterIdUsuarioPorAluno(Guid idAluno);
    }
}