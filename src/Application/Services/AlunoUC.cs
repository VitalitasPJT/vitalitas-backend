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
            var sucesso = _alunoRepository.AtualizarObjetivo(idusuario, objetivo);
            if (!sucesso)
            {
                throw new Exception("Erro ao atualizar objetivo");
            }
            var status = new StatusHTTP("Objetivo atualizado com sucesso", 200, true);
            return new AtualizarObjetivoResponse(status);
        }

        public ListarAlunoResponse ListarAluno(Guid idaluno)
        {
            try
            {
                var result = _alunoRepository.ListarAluno(idaluno);
                var aluno = new AlunoDTO
                {
                    IdAluno = result.idAluno,
                    IdAcademia = result.idAcademia,
                    IdUsuario = result.idUsuario,
                    Objetivo = result.objetivo,
                    Nome = result.nome,
                    Email = result.email,
                    TipoUsuario = (Domain.Enums.TipoUsuario)result.tipoUsuario,
                    StatusPagamento = "Pago"
                };
                var status = new StatusHTTP("Aluno encontrado com sucesso", 200, true);
                return new ListarAlunoResponse(aluno, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public StatusHTTP TrocarSenha(Guid idusaurio, string novasenha)
        {
            var sucesso = _alunoRepository.TrocarSenha(idusaurio, novasenha);
            if (!sucesso)
            {
                throw new Exception("Erro ao trocar senha");
            }
            var status = new StatusHTTP("Senha trocada com sucesso", 200, true);
            return status;
        }

        /*public CriarAlunoResponse CriarAluno(Guid idinstrutor, Guid idusuario, int idcontrato, Guid idacademia, string objetivo)
        {
            var idaluno = _alunoRepository.CriarAluno(idinstrutor, idusuario, idcontrato, idacademia, objetivo);
            return new CriarAlunoResponse(idaluno, new StatusHTTP("Aluno criado com sucesso", 201, true));
        }

        */


        public VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor)
        {
            var sucesso = _alunoRepository.VincularInstrutor(idaluno, idinstrutor);
            if (!sucesso)
            {
                throw new Exception("Erro ao vincular instrutor");
            }
            var status = new StatusHTTP("Instrutor vinculado com sucesso", 200, true);
            return new VincularInstrutorResponse(status);
        }
    }
}
