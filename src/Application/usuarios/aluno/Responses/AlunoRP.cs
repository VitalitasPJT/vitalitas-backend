using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.DTOs
{
    public class AlunoRP
    {
        public class ListarAlunoResponse
        {
            public ListarAlunoResponse(AlunoDTO aluno, StatusHTTP status)
            {
                Aluno = aluno;
                this.Status = status;
            }

            public AlunoDTO Aluno { get; set; }
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

        public class TrocarSenhaResponse
        {
            public TrocarSenhaResponse(StatusHTTP status)
            {
                this.Status = status;
            }

            public StatusHTTP Status { get; set; }
        }


    }
}