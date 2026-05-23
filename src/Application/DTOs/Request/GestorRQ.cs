using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Request
{
    public class GestorRQ
    {
        public class CriarUsuarioRequest
        {
            public Guid IdAcademia { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public DateOnly DataNascimento { get; set; }
            public string Cpf { get; set; }
            public TipoUsuario TipoUsuario { get; set; }
            public string Quadra { get; set; }
            public string Rua { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string Estado { get; set; }
            public string Cep { get; set; }
        }
        
        public class CriarAlunoRequest
        {
            public Guid IdUsuario { get; set; }
            public Guid IdInstrutor { get; set; }
            public Guid IdContrato { get; set; }
            public required string Objetivo { get; set; }
        }

        public class CriarInstrutorRequest
        {
            public Guid IdUsuario { get; set; }
            public string CREF { get; set; }
        }

        public class CriarFuncionarioRequest
        {
            public Guid IdUsuario { get; set; }
            public string Cargo { get; set; }
        }

        public class CriarGestorRequest
        {
            public Guid IdUsuario { get; set; }
        }

        public class ObterGestorRequest
        {
            public Guid IdGestor { get; set; }
        }


        public class ListarAlunosEUsuariosRequest
        {
            public required Guid IdAcademia { get; set; }
        }

        public class ListarUsuarioRequest
        {
            public required Guid IdUsuario { get; set; }
        }
    }
}