using System.Runtime.InteropServices;
using Application.DTOs;
using Application.DTOs.Request;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using static Application.DTOs.AlunoRP;
using static Application.DTOs.Response.GertorRP;
using static DTOs.Constructor.Constructor;

namespace Application.Services
{
    public class GestorUC : IGestorUseCase
    {
        private readonly IGestorRepository _gestorRepository;

        public GestorUC(IGestorRepository gestorrepository)
        {
            _gestorRepository = gestorrepository;
        }

        public CriarUsuarioResponse CriarUsuario(ConstructorUsuario usuario)
        {
            var novoUsuario = new Usuario(
                usuario.IdUsuario,
                usuario.IdAcademia,
                usuario.Nome.Valor,
                usuario.Email,
                usuario.Senha,
                usuario.DataNascimento,
                usuario.CPF,
                usuario.TipoUsuario,
                usuario.Ativo,
                usuario.Flag,
                usuario.Quadra,
                usuario.Rua,
                usuario.Bairro,
                usuario.Cidade,
                usuario.Estado,
                usuario.CEP
            );
            var idUsuario = _gestorRepository.CriarUsuario(novoUsuario);
            return new CriarUsuarioResponse(idUsuario, new StatusHTTP("Usuário criado com sucesso", 201, true));
        }

      
        /*public AtualizarObjetivoResponse AtualizarObjetivo(Guid idusuario, string objetivo)
        {
            var sucesso = _gestorRepository.AtualizarObjetivo(idusuario, objetivo);
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

        public VincularInstrutorResponse VincularInstrutor(Guid idaluno, Guid idinstrutor)
        {
            var sucesso = _alunoRepository.VincularInstrutor(idaluno, idinstrutor);
            if (!sucesso)
            {
                throw new Exception("Erro ao vincular instrutor");
            }
            var status = new StatusHTTP("Instrutor vinculado com sucesso", 200, true);
            return new VincularInstrutorResponse(status);
        }*/
    }
}
