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

        public class CriarFuncionarioResponse
        {
            public CriarFuncionarioResponse(Guid idFuncionario, StatusHTTP statusHTTP)
            {
                IdFuncionario = idFuncionario;
                Status = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
            public Guid IdFuncionario { get; set; }
        }

        public class CriarGestorResponse
        {
            public CriarGestorResponse(Guid idGestor, StatusHTTP statusHTTP)
            {
                IdGestor = idGestor;
                Status = statusHTTP;
            }

            public StatusHTTP Status { get; set; }
            public Guid IdGestor { get; set; }
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

        public class ListarUsuariosResponse
        {
            public ListarUsuariosResponse(List<UsuarioDTO> usuarios, StatusHTTP status)
            {
                Usuarios = usuarios;
                Status = status;
            }

            public List<UsuarioDTO> Usuarios { get; set; }
            public StatusHTTP Status { get; set; }
        }

        public class ObterGestorResponse
        {
            public ObterGestorResponse(GestorDTO? gestor, StatusHTTP status)
            {
                Gestor = gestor;
                Status = status;
            }

            public GestorDTO? Gestor { get; set; }
            public StatusHTTP Status { get; set; }
        }
        public class ListarUsuarioResponse
        {
            public ListarUsuarioResponse(dynamic usuario, StatusHTTP status)
            {
                Usuario = usuario;
                Status = status;
            }

            public dynamic Usuario { get; set; }
            public StatusHTTP Status { get; set; }
        }
    }
}