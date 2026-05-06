using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AlunoRQ
    {
        public class CriarAlunoRequest
        {
            public Guid IdInstrutor { get; set; }
            public Guid IdUsuario { get; set; }
            public int IdContrato { get; set; }
            public Guid IdAcademia { get; set; }
            public required string Objetivo { get; set; }
        } 

        public class ListarAlunosRequest
        {
            public Guid IdAcademia { get; set; }
        }

        public class ListarAlunoRequest
        {
            public Guid IdAluno { get; set; }
        }

        public class VincularInstrutorRequest
        {
            public Guid IdAluno { get; set; }
            public Guid IdInstrutor { get; set; }
        }

        public class AtualizarObjetivoRequest
        {
            public Guid IdAluno { get; set; }
            public required string NovoObjetivo { get; set; }
        }
    }
}
