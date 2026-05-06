using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using static Application.DTOs.AlunoRP;

namespace Application.Services
{
    public class AlunoUC : IAlunoUseCase
    {
        private readonly IAluno _alunoRepository;

        public AlunoUC(IAluno alunorepository)
        {
            _alunoRepository = alunorepository;
        }

        public AtualizarObjetivoResponse AtualizarObjetivo(Guid idusuario, string objetivo)
        {
            throw new NotImplementedException();
        }

        public CriarAlunoResponse CriarAluno(Guid idinstrutor, Guid idusuario, int idcontrato, Guid idacademia, string objetivo)
        {
            throw new NotImplementedException();
        }

        public ListarAlunoResponse ListarAluno(Guid idaluno)
        {
            throw new NotImplementedException();
        }

        /*public CriarAlunoResponse CriarAluno(Guid idinstrutor, Guid idusuario, int idcontrato, Guid idacademia, string objetivo)
        {
            var idaluno = _alunoRepository.CriarAluno(idinstrutor, idusuario, idcontrato, idacademia, objetivo);
            return new CriarAlunoResponse(idaluno, new StatusHTTP("Aluno criado com sucesso", 201, true));
        }

        public ListarAlunoResponse ListarAluno(Guid idaluno)
        {
            try
            {
                var result = _alunoRepository.ListarAluno(idaluno);
                var aluno = new AlunoDto
                {
                    idAluno = result.idAluno,
                    idAcademia = result.idAcademia,
                    idUsuario = result.idUsuario,
                    objetivo = result.objetivo,
                    nome = result.nome,
                    email = result.email,
                    tipoUsuario = (Domain.Enums.TipoUsuario)result.tipoUsuario,
                    statusPagamento = "Pago"
                };
                var status =new StatusHTTP("Aluno encontrado com sucesso", 200, true);
                return new ListarAlunoResponse(aluno, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }*/
        public ListarAlunosResponse ListarAlunos(Guid idacademia)
        {
            throw new NotImplementedException();
        }

        public VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor)
        {
            throw new NotImplementedException();
        }
    }
}
