using System.Runtime.InteropServices;
using Application.Compartilhado;
using Application.Usuarios.Aluno.DTO;
using Application.Usuarios.Common.DTO;
using Application.Usuarios.Gestor.DTO;
using Application.Usuarios.Gestor.Request;
using Application.Usuarios.Gestor.Interfaces;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Aluno.Entities;
using Domain.Features.Usuarios.Instrutor.Entities;
using Domain.Enums;
using Domain.Features.Usuarios.Gestor.Interfaces;
using static Application.Usuarios.Aluno.Response.AlunoRP;
using Application.Usuarios.Gestor.Response;
using Application.Usuarios.Common.Constructor;
using Application.Usuarios.Aluno.Constructor;
using Application.Usuarios.Instrutor.Constructor;
using Application.Usuarios.Funcionario.Constructor;
using Application.Usuarios.Gestor.Constructor;

namespace Application.Usuarios.Gestor.UseCases
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
            var novoAluno = new Domain.Features.Usuarios.Aluno.Entities.Aluno(
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
            var novoInstrutor = new Domain.Features.Usuarios.Instrutor.Entities.Instrutor(
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

        public CriarFuncionarioResponse CriarFuncionario(ConstructorFuncionario funcionario)
        {
            var (sucesso, idFuncionario) = _gestorRepository.CriarFuncionario(funcionario.IdUsuario, funcionario.Cargo);
            if (!sucesso)
            {
                var status = new StatusHTTP("Erro ao criar funcionário", 500, false);
                var response = new CriarFuncionarioResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new StatusHTTP("Funcionário criado com sucesso", 201, true);
            var response_success = new CriarFuncionarioResponse(idFuncionario, status_success);
            return response_success;
        }

        public CriarGestorResponse CriarGestor(ConstructorGestor gestor)
        {
            var (sucesso, idGestor) = _gestorRepository.CriarGestor(gestor.IdUsuario);
            if (!sucesso)
            {
                var status = new StatusHTTP("Erro ao criar gestor", 500, false);
                var response = new CriarGestorResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new StatusHTTP("Gestor criado com sucesso", 201, true);
            var response_success = new CriarGestorResponse(idGestor, status_success);
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

        public ListarUsuariosResponse ListarUsuarios(Guid idAcademia)
        {
            try
            {
                var result = _gestorRepository.ListarUsuarios(idAcademia);
                var usuarios = new List<UsuarioDTO>();
                for (int i = 0; i < result.Count; i++)
                {
                    var usuario = new UsuarioDTO
                    {
                        IdUsuario = result[i].idUsuario,
                        IdAcademia = result[i].idAcademia,
                        Nome = result[i].nome,
                        Email = result[i].email,
                        TipoUsuario = (TipoUsuario)result[i].tipoUsuario,
                        Ativo = result[i].ativo
                    };
                    usuarios.Add(usuario);
                }

                if (usuarios == null || usuarios.Count == 0)
                {
                    var status_empty = new StatusHTTP("Nenhum usuário encontrado", 404, false);
                    return new ListarUsuariosResponse(new List<UsuarioDTO>(), status_empty);
                }

                var status = new StatusHTTP("Usuários listados com sucesso", 200, true);
                return new ListarUsuariosResponse(usuarios, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ObterGestorResponse ObterGestor(Guid idUsuario)
        {
            try
            {
                var result = _gestorRepository.ObterGestor(idUsuario);
                var gestor = new GestorDTO
                {
                    IdUsuario = result.idUsuario,
                    IdGestor = result.idGestor,
                    IdAcademia = result.idAcademia,
                    Nome = result.nome,
                    Email = result.email,
                    CPF = result.CPF
                };

                if (gestor == null)
                {
                    var status_empty = new StatusHTTP("Nenhum gestor encontrado", 404, false);
                    return new ObterGestorResponse(null, status_empty);
                }

                var status = new StatusHTTP("Gestor obtido com sucesso", 200, true);
                return new ObterGestorResponse(gestor, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ListarUsuarioResponse ListarUsuario(Guid idUsuario)
        {
            try
            {
                var (result, tipoUsuario) = _gestorRepository.ListarUsuario(idUsuario);
                if (result == null)
                {
                    var status_empty = new StatusHTTP("Nenhum usuário encontrado", 404, false);
                    return new ListarUsuarioResponse(null!, status_empty);
                }
                dynamic? usuario = null;
                // Números de case alinhados com o enum real Domain.Enums.TipoUsuario
                // (Instrutor=1, Aluno=2, Gestor=3, Administrador=4).
                switch (tipoUsuario)
                {
                    case (int)TipoUsuario.Instrutor:
                        usuario = new
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (TipoUsuario)result.tipoUsuario,
                            Ativo = result.ativo,
                            Quadra = result.quadra,
                            Rua = result.rua,
                            Bairro = result.bairro,
                            Cidade = result.cidade,
                            Estado = result.estado,
                            CEP = result.cep,
                            CREF = result.cref
                        };
                        break;
                    case (int)TipoUsuario.Aluno:
                        usuario = new
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (TipoUsuario)result.tipoUsuario,
                            Ativo = result.ativo,
                            Quadra = result.quadra,
                            Rua = result.rua,
                            Bairro = result.bairro,
                            Cidade = result.cidade,
                            Estado = result.estado,
                            CEP = result.cep,
                            Objetivo = result.objetivo
                        };
                        break;
                    case (int)TipoUsuario.Gestor:
                        usuario = new
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (TipoUsuario)result.tipoUsuario,
                            Ativo = result.ativo,
                            Quadra = result.quadra,
                            Rua = result.rua,
                            Bairro = result.bairro,
                            Cidade = result.cidade,
                            Estado = result.estado,
                            CEP = result.cep
                        };
                        break;
                    case (int)TipoUsuario.Administrador:
                        usuario = new
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (TipoUsuario)result.tipoUsuario,
                            Ativo = result.ativo,
                            Quadra = result.quadra,
                            Rua = result.rua,
                            Bairro = result.bairro,
                            Cidade = result.cidade,
                            Estado = result.estado,
                            CEP = result.cep,
                            Cargo = result.cargo
                        };
                        break;
                }

                var status = new StatusHTTP("Usuário obtido com sucesso", 200, true);
                return new ListarUsuarioResponse(usuario, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
