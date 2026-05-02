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
        CriarAlunoResponse CriarAluno(Guid idinstrutor, Guid idusuario, int idcontrato, Guid idacademia, string objetivo);

        ListarAlunosResponse ListarAlunos(Guid idacademia);

        ListarAlunoResponse ListarAluno(Guid idaluno);

        VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor);

        AtualizarObjetivoResponse AtualizarObjetivo(Guid idusuario, string objetivo);
    }
}