using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.DTOs
{
    public class AlunoRP
    {
        public class CriarAlunoResponse
        {
            public CriarAlunoResponse(Guid idAluno, StatusHTTP status)
            {
                IdAluno = idAluno;
                this.Status = status;
            }

            public Guid IdAluno { get; set; }
            public StatusHTTP Status { get; set; }
        }

        public class ListarAlunosResponse
        {
            public List<AlunoDto> Alunos { get; set; } = new List<AlunoDto>();
            public required StatusHTTP Status { get; set; }
        }

        public class ListarAlunoResponse
        {
            public ListarAlunoResponse(AlunoDto aluno, StatusHTTP status)
            {
                Aluno = aluno;
                this.Status = status;
            }

            public AlunoDto Aluno { get; set; }
            public StatusHTTP Status { get; set; }
        }

        public class VincularInstrutorResponse
        {
            public VincularInstrutorResponse(StatusHTTP status)
            {
                this.Status = status;
            }

            public StatusHTTP Status { get; set; }
        }

        public class AtualizarObjetivoResponse
        {
            public AtualizarObjetivoResponse(StatusHTTP status)
            {
                this.Status = status;
            }

            public StatusHTTP Status { get; set; }
        }

        // --- DTO Auxiliar para representar um Aluno ---
        public class AlunoDto
        {
            public Guid idAluno { get; set; }
            public Guid idAcademia { get; set; }
            public Guid idUsuario { get; set; }
            public TipoUsuario tipoUsuario { get; set; }
            public required string objetivo { get; set; }
            public required string nome { get; set; }
            public required string email { get; set; }
            public required string statusPagamento { get; set; } 
        }
    }
}