using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.DTOs.AlunoRP;

namespace Application.DTOs.Response
{
    public class GertorRP
    {
        public class CriarUsuarioResponse
        {
            public CriarUsuarioResponse(Guid idUsuario, StatusHTTP statusHTTP)
            {
                IdUsuario = idUsuario;
                Status = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
            public Guid IdUsuario { get; set; }
        }

        public class CriarAlunoResponse
        {
            public CriarAlunoResponse(Guid idAluno, StatusHTTP statusHTTP)
            {
                IdAluno = idAluno;
                Status = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
            public Guid IdAluno { get; set; }
        }

        public class CriarInstrutorResponse
        {
            public CriarInstrutorResponse(Guid idInstrutor, StatusHTTP statusHTTP)
            {
                IdInstrutor = idInstrutor;
                Status = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
            public Guid IdInstrutor { get; set; }
        }

        public class ListarAlunosResponse
        {
            public ListarAlunosResponse(List<AlunoDTO> alunos, StatusHTTP status)
            {
                Alunos = alunos;
                Status = status;
            }

            public List<AlunoDTO> Alunos { get; set; }
            public StatusHTTP Status { get; set; }
        }
    }
}