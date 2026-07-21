using System;
using Application.Compartilhado;

namespace Application.Usuarios.Gestor.Response
{
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
}
