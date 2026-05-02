using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.DTOs
{
    public class AlunoRP
    {
        public class CriarAlunoResponse
        {
            private StatusHTTP statusHTTP;

            public CriarAlunoResponse(Guid idAluno, StatusHTTP statusHTTP)
            {
                IdAluno = idAluno;
                this.statusHTTP = statusHTTP;
            }

            public Guid IdAluno { get; set; }
            public StatusHTTP Status { get; set; }
        }

        public class ListarAlunosResponse
        {
            public List<AlunoDto> Alunos { get; set; } = new List<AlunoDto>();
            public StatusHTTP Status { get; set; }
        }

        public class ListarAlunoResponse
        {
            private StatusHTTP status;

            public ListarAlunoResponse(AlunoDto aluno, StatusHTTP status)
            {
                Aluno = aluno;
                this.status = status;
            }

            public AlunoDto Aluno { get; set; }
            public StatusHTTP statusHTTP { get; set; }
        }

        public class VincularInstrutorResponse
        {
            private StatusHTTP statusHTTP;

            public VincularInstrutorResponse(StatusHTTP statusHTTP)
            {
                this.statusHTTP = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
        }

        public class AtualizarObjetivoResponse
        {
            private StatusHTTP statusHTTP;

            public AtualizarObjetivoResponse(StatusHTTP statusHTTP)
            {
                this.statusHTTP = statusHTTP;
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
            public string objetivo { get; set; }
            public string nome { get; set; }
            public string email { get; set; }
            public string statusPagamento { get; set; } 
        }
    }
}