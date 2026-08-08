using Application.Shared;
using Application.Features.Users.Member.DTO;
using Application.Features.Users.Member.Interfaces;
using Domain.Enums;
using Domain.Features.Users.Member.Interfaces;
using static Application.Features.Users.Member.Response.MemberRP;

namespace Application.Features.Users.Member.UseCases
{
    public class MemberUC : IMemberUseCase
    {
        private readonly IMemberRepository _alunoRepository;

        public MemberUC(IMemberRepository alunorepository)
        {
            _alunoRepository = alunorepository;
        }

        public UpdateGoalResponse AtualizarObjetivo(Guid idusuario, string objetivo)
        {
            var sucesso = _alunoRepository.AtualizarObjetivo(idusuario, objetivo);
            if (!sucesso)
            {
                throw new Exception("Erro ao atualizar objetivo");
            }
            var status = new HttpStatus("Objetivo atualizado com sucesso", 200, true);
            return new UpdateGoalResponse(status);
        }

        public ListMemberResponse ListarAluno(Guid idaluno)
        {
            try
            {
                var result = _alunoRepository.ListarAluno(idaluno);
                var aluno = new MemberDTO
                {
                    IdAluno = result.idAluno,
                    IdAcademia = result.idAcademia,
                    IdUsuario = result.idUsuario,
                    Objetivo = result.objetivo,
                    Nome = result.nome,
                    Email = result.email,
                    TipoUsuario = (Domain.Enums.UserType)result.tipoUsuario,
                    StatusPagamento = "Pago"
                };
                var status = new HttpStatus("Aluno encontrado com sucesso", 200, true);
                return new ListMemberResponse(aluno, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public HttpStatus TrocarSenha(Guid idusaurio, string novasenha)
        {
            var sucesso = _alunoRepository.TrocarSenha(idusaurio, novasenha);
            if (!sucesso)
            {
                throw new Exception("Erro ao trocar senha");
            }
            var status = new HttpStatus("Senha trocada com sucesso", 200, true);
            return status;
        }

        public LinkInstructorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor)
        {
            var sucesso = _alunoRepository.VincularInstrutor(idaluno, idinstrutor);
            if (!sucesso)
            {
                throw new Exception("Erro ao vincular instrutor");
            }
            var status = new HttpStatus("Instrutor vinculado com sucesso", 200, true);
            return new LinkInstructorResponse(status);
        }

        public Guid? ObterIdUsuarioPorAluno(Guid idAluno)
            => _alunoRepository.ObterIdUsuarioPorAluno(idAluno);
    }
}
