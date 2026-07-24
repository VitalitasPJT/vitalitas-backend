using System.Collections.Generic;
using Application.Compartilhado;
using Application.Usuarios.Aluno.DTO;

namespace Application.Usuarios.Gestor.Response
{
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
