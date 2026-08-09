using System.Runtime.InteropServices;
using Application.Shared;
using Application.Features.Users.Member.DTO;
using Application.Features.Users.Common.DTO;
using Application.Features.Users.Manager.DTO;
using Application.Features.Users.Manager.Request;
using Application.Features.Users.Manager.Interfaces;
using Domain.Features.Users.Common.Entities;
using Domain.Features.Users.Member.Entities;
using Domain.Features.Users.Instructor.Entities;
using Domain.Enums;
using Domain.Features.Users.Manager.Interfaces;
using static Application.Features.Users.Member.Response.MemberRP;
using Application.Features.Users.Manager.Response;
using Application.Features.Users.Common.Constructor;
using Application.Features.Users.Member.Constructor;
using Application.Features.Users.Instructor.Constructor;
using Application.Features.Users.Employee.Constructor;
using Application.Features.Users.Manager.Constructor;

namespace Application.Features.Users.Manager.UseCases
{
    public class ManagerUC : IManagerUseCase
    {
        private readonly IManagerRepository _gestorRepository;

        public ManagerUC(IManagerRepository gestorrepository)
        {
            _gestorRepository = gestorrepository;
        }

        public CreateUserResponse CriarUsuario(ConstructorUser usuario)
        {
            var novoUsuario = new User(
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
                var status = new HttpStatus("Erro ao criar usuário", 500, false);
                var response = new CreateUserResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new HttpStatus("Usuário criado com sucesso", 201, true);
            var response_success = new CreateUserResponse(idUsuario, status_success);
            return response_success;
        }

        public CreateMemberResponse CriarAluno(ConstructorMember aluno)
        {
            var novoAluno = new Domain.Features.Users.Member.Entities.Member(
                aluno.IdAluno,
                aluno.IdUsuario,
                aluno.IdInstrutor,
                aluno.IdContrato,
                aluno.Objetivo
            );
            var (sucesso, idAluno) = _gestorRepository.CriarAluno(novoAluno);
            if (!sucesso)
            {
                var status = new HttpStatus("Erro ao criar aluno", 500, false);
                var response = new CreateMemberResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new HttpStatus("Aluno criado com sucesso", 201, true);
            var response_success = new CreateMemberResponse(idAluno, status_success);
            return response_success;
        }

        public CreateInstructorResponse CriarInstrutor(ConstructorInstructor instrutor)
        {
            var novoInstrutor = new Domain.Features.Users.Instructor.Entities.Instructor(
                instrutor.IdInstrutor,
                instrutor.IdUsuario,
                instrutor.CREF
            );
            var (sucesso, idInstrutor) = _gestorRepository.CriarInstrutor(novoInstrutor);
            if (!sucesso)
            {
                var status = new HttpStatus("Erro ao criar instrutor", 500, false);
                var response = new CreateInstructorResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new HttpStatus("Instrutor criado com sucesso", 201, true);
            var response_success = new CreateInstructorResponse(idInstrutor, status_success);
            return response_success;
        }

        public CreateEmployeeResponse CriarFuncionario(ConstructorEmployee funcionario)
        {
            var (sucesso, idFuncionario) = _gestorRepository.CriarFuncionario(funcionario.IdUsuario, funcionario.Cargo);
            if (!sucesso)
            {
                var status = new HttpStatus("Erro ao criar funcionário", 500, false);
                var response = new CreateEmployeeResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new HttpStatus("Funcionário criado com sucesso", 201, true);
            var response_success = new CreateEmployeeResponse(idFuncionario, status_success);
            return response_success;
        }

        public CreateManagerResponse CriarGestor(ConstructorManager gestor)
        {
            var (sucesso, idGestor) = _gestorRepository.CriarGestor(gestor.IdUsuario);
            if (!sucesso)
            {
                var status = new HttpStatus("Erro ao criar gestor", 500, false);
                var response = new CreateManagerResponse(Guid.Empty, status);
                return response;
            }
            var status_success = new HttpStatus("Gestor criado com sucesso", 201, true);
            var response_success = new CreateManagerResponse(idGestor, status_success);
            return response_success;
        }

        public ListMembersResponse ListarAlunos(Guid idAcademia)
        {
            try
            {
                var result = _gestorRepository.ListarAlunos(idAcademia);
                var alunos = new List<MemberDTO>();
                for (int i = 0; i < result.Count; i++)
                {
                    var aluno = new MemberDTO
                    {
                        IdAluno = result[i].idAluno,
                        IdAcademia = result[i].idAcademia,
                        IdUsuario = result[i].idUsuario,
                        TipoUsuario = (UserType)result[i].tipoUsuario,
                        Objetivo = result[i].objetivo,
                        Nome = result[i].nome,
                        Email = result[i].email,
                        StatusPagamento = "Pago"
                    };
                    alunos.Add(aluno);
                }

                if (alunos == null || alunos.Count == 0)
                {
                    var status_empty = new HttpStatus("Nenhum aluno encontrado", 404, false);
                    return new ListMembersResponse(new List<MemberDTO>(), status_empty);
                }

                var status = new HttpStatus("Alunos listados com sucesso", 200, true);
                return new ListMembersResponse(alunos, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ListUsersResponse ListarUsuarios(Guid idAcademia)
        {
            try
            {
                var result = _gestorRepository.ListarUsuarios(idAcademia);
                var usuarios = new List<UserDTO>();
                for (int i = 0; i < result.Count; i++)
                {
                    var usuario = new UserDTO
                    {
                        IdUsuario = result[i].idUsuario,
                        IdAcademia = result[i].idAcademia,
                        Nome = result[i].nome,
                        Email = result[i].email,
                        TipoUsuario = (UserType)result[i].tipoUsuario,
                        Ativo = result[i].ativo
                    };
                    usuarios.Add(usuario);
                }

                if (usuarios == null || usuarios.Count == 0)
                {
                    var status_empty = new HttpStatus("Nenhum usuário encontrado", 404, false);
                    return new ListUsersResponse(new List<UserDTO>(), status_empty);
                }

                var status = new HttpStatus("Usuários listados com sucesso", 200, true);
                return new ListUsersResponse(usuarios, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetManagerResponse ObterGestor(Guid idUsuario)
        {
            try
            {
                var result = _gestorRepository.ObterGestor(idUsuario);
                var gestor = new ManagerDTO
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
                    var status_empty = new HttpStatus("Nenhum gestor encontrado", 404, false);
                    return new GetManagerResponse(null, status_empty);
                }

                var status = new HttpStatus("Gestor obtido com sucesso", 200, true);
                return new GetManagerResponse(gestor, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ListUserResponse ListarUsuario(Guid idUsuario)
        {
            try
            {
                var (result, tipoUsuario) = _gestorRepository.ListarUsuario(idUsuario);
                if (result == null)
                {
                    var status_empty = new HttpStatus("Nenhum usuário encontrado", 404, false);
                    return new ListUserResponse(null!, status_empty);
                }
                dynamic? usuario = null;
                switch (tipoUsuario)
                {
                    case 1:
                        usuario = new 
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (UserType)result.tipoUsuario,
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
                    case 2:
                        usuario = new 
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (UserType)result.tipoUsuario,
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
                    case 3:
                        usuario = new 
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (UserType)result.tipoUsuario,
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
                    case 4:
                        usuario = new 
                        {
                            IdUsuario = result.idUsuario,
                            IdAcademia = result.idAcademia,
                            Nome = result.nome,
                            Email = result.email,
                            TipoUsuario = (UserType)result.tipoUsuario,
                            Ativo = result.ativo,
                            Quadra = result.quadra,
                            Rua = result.rua,
                            Bairro = result.bairro,
                            Cidade = result.cidade,
                            Estado = result.estado,
                            CEP = result.cep
                        };
                        break;
                }

                var status = new HttpStatus("Usuário obtido com sucesso", 200, true);
                return new ListUserResponse(usuario, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
