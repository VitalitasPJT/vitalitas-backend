using Application.DTOs;
using Domain.Entities; 
using Domain.Enums;
using System;
using System.Collections.Generic;
using static Application.DTOs.AlunoRP;
using static Application.DTOs.UsuarioRS;

namespace Application.Interfaces
{
    public interface IAlunoUseCase
    {
        ListarAlunoResponse ListarAluno(Guid idaluno);

        VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor);

        AtualizarObjetivoResponse AtualizarObjetivo(Guid idusuario, string objetivo);
        StatusHTTP TrocarSenha(Guid idusuario, string novaSenha);
    }
}