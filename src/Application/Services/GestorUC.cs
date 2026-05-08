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
            var (sucesso, idUsuario) = _gestorRepository.CriarUsuario(novoUsuario);
            if (!sucesso)
            {
                var status = new StatusHTTP("Erro ao criar usuário", 500, false);
                var response = new CriarUsuarioResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new StatusHTTP("Usuário criado com sucesso", 201, true);
            var response_success = new CriarUsuarioResponse(idUsuario, status_success);
            return response_success;
        }

        public CriarAlunoResponse CriarAluno(ConstructorAluno aluno)
        {
            var novoAluno = new Aluno(
                aluno.IdAluno,
                aluno.IdUsuario,
                aluno.IdInstrutor,
                aluno.IdContrato,
                aluno.Objetivo
            );
            var (sucesso, idAluno) = _gestorRepository.CriarAluno(novoAluno);
            if (!sucesso)
            {
                var status = new StatusHTTP("Erro ao criar aluno", 500, false);
                var response = new CriarAlunoResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new StatusHTTP("Aluno criado com sucesso", 201, true);
            var response_success = new CriarAlunoResponse(idAluno, status_success);
            return response_success;
        }

        public CriarInstrutorResponse CriarInstrutor(ConstructorInstrutor instrutor)
        {
            var novoInstrutor = new Instrutor(
                instrutor.IdInstrutor,
                instrutor.IdUsuario,
                instrutor.CREF
            );
            var (sucesso, idInstrutor) = _gestorRepository.CriarInstrutor(novoInstrutor);
            if (!sucesso)
            {
                var status = new StatusHTTP("Erro ao criar instrutor", 500, false);
                var response = new CriarInstrutorResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new StatusHTTP("Instrutor criado com sucesso", 201, true);
            var response_success = new CriarInstrutorResponse(idInstrutor, status_success);
            return response_success;
        }

        public ListarAlunosResponse ListarAlunos(Guid idAcademia)
        {
            try
            {
                var result = _gestorRepository.ListarAlunos(idAcademia);
                var alunos = new List<AlunoDTO>();
                for (int i = 0; i < result.Count; i++)
                {
                    var aluno = new AlunoDTO
                    {
                        IdAluno = result[i].idAluno,
                        IdAcademia = result[i].idAcademia,
                        IdUsuario = result[i].idUsuario,
                        TipoUsuario = (TipoUsuario)result[i].tipoUsuario,
                        Objetivo = result[i].objetivo,
                        Nome = result[i].nome,
                        Email = result[i].email,
                        StatusPagamento = "Pago"
                    };
                    alunos.Add(aluno);
                }

                if (alunos == null || alunos.Count == 0)
                {
                    var status_empty = new StatusHTTP("Nenhum aluno encontrado", 404, false);
                    return new ListarAlunosResponse(new List<AlunoDTO>(), status_empty);
                }

                var status = new StatusHTTP("Alunos listados com sucesso", 200, true);
                return new ListarAlunosResponse(alunos, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

       

        public StatusHTTP TrocarSenha(Guid idusaurio, string novasenha)
        {
            var sucesso = _alunoRepository.TrocarSenha(idusaurio, novasenha);
            if (!sucesso)
            {
                throw new Exception("Erro ao trocar senha");
            }
            var status = new StatusHTTP("Senha trocada com sucesso", 200, true);
            return status;
        }*/
    }
}
