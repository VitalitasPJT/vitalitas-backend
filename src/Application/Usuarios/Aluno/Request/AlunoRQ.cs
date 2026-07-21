using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Aluno.Request
{
    public class AlunoRQ
    {
         
        public class ListarAlunoRequest
        {
            public required Guid IdAluno { get; set; }
        }

        public class VincularInstrutorRequest
        {
            public Guid IdAluno { get; set; }
            public Guid IdInstrutor { get; set; }
        }

        public class AtualizarObjetivoRequest
        {
            public Guid IdAluno { get; set; }
            public string NovoObjetivo { get; set; }
        }

        public class TrocarSenhaRequest
        {
            public required Guid IdUsuario { get; set; }
            public required string NovaSenha { get; set; }
        }
    }
}
