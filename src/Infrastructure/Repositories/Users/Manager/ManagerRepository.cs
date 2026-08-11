using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Features.Users.Common.Entities;
using Domain.Features.Users.Instructor.Entities;
using Domain.Enums;
using Domain.Features.Users.Manager.Interfaces;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.Users.Manager
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public ManagerRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Insere só o User, na conexão/transação já aberta pelo chamador — os três
        // métodos CriarUsuarioX reaproveitam isso para que User + perfil específico
        // sejam uma única transação atômica (sem User órfão se o segundo insert falhar).
        private static int InserirUsuario(IDbConnection connection, IDbTransaction transaction, User usuario)
        {
            string query = @"INSERT INTO [dbo].[User] (idUsuario, idAcademia, nome, email, senha, dataNascimento, cpf, tipoUsuario, ativo, flag, quadra, rua, bairro, cidade, estado, cep)
                            VALUES (@IdUsuario, @IdAcademia, @Nome, @Email, @Senha, @DataNascimento, @CPF, @TipoUsuario, @Ativo, @Flag, @Quadra, @Rua, @Bairro, @Cidade, @Estado, @CEP)";

            var parameters = new
            {
                IdUsuario = usuario.IdUsuario,
                IdAcademia = usuario.IdAcademia,
                Nome = usuario.Nome.Valor,
                Email = usuario.Email.Valor,
                Senha = usuario.Senha,
                DataNascimento = usuario.DataNascimento.ToDateTime(TimeOnly.MinValue),
                CPF = usuario.CPF.Valor,
                TipoUsuario = usuario.TipoUsuario,
                Ativo = usuario.Ativo,
                Flag = usuario.Flag,
                Quadra = usuario.Quadra,
                Rua = usuario.Rua,
                Bairro = usuario.Bairro,
                Cidade = usuario.Cidade,
                Estado = usuario.Estado,
                CEP = usuario.CEP
            };

            return connection.Execute(query, parameters, transaction);
        }

        public (bool sucesso, Guid idUsuario, Guid idAluno) CriarUsuarioAluno(User usuario, Domain.Features.Users.Member.Entities.Member aluno)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var userRows = InserirUsuario(connection, transaction, usuario);

            string queryMember = @"INSERT INTO [dbo].[Member] (idAluno, idInstrutor, idUsuario, idContrato, objetivo)
                            VALUES (@IdAluno, @IdInstrutor, @IdUsuario, @IdContrato, @Objetivo)";
            var memberRows = connection.Execute(queryMember, new
            {
                aluno.IdAluno,
                aluno.IdInstrutor,
                aluno.IdUsuario,
                aluno.IdContrato,
                aluno.Objetivo
            }, transaction);

            if (userRows == 0 || memberRows == 0)
            {
                transaction.Rollback();
                return (false, Guid.Empty, Guid.Empty);
            }

            transaction.Commit();
            return (true, usuario.IdUsuario, aluno.IdAluno);
        }

        public (bool sucesso, Guid idUsuario, Guid idInstrutor) CriarUsuarioInstrutor(User usuario, Instructor instrutor)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var userRows = InserirUsuario(connection, transaction, usuario);

            string queryInstrutor = @"INSERT INTO [dbo].[Instructor] (idInstrutor, idUsuario, cref)
                            VALUES (@IdInstrutor, @IdUsuario, @CREF)";
            var instrutorRows = connection.Execute(queryInstrutor, new
            {
                instrutor.IdInstrutor,
                instrutor.IdUsuario,
                CREF = instrutor.CREF.Valor
            }, transaction);

            if (userRows == 0 || instrutorRows == 0)
            {
                transaction.Rollback();
                return (false, Guid.Empty, Guid.Empty);
            }

            transaction.Commit();
            return (true, usuario.IdUsuario, instrutor.IdInstrutor);
        }

        public (bool sucesso, Guid idUsuario, Guid idFuncionario) CriarUsuarioAdministrador(User usuario, Role cargo)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var userRows = InserirUsuario(connection, transaction, usuario);

            string queryFuncionario = @"INSERT INTO [dbo].[Employee] (idFuncionario, idUsuario, cargo)
                            VALUES (@IdFuncionario, @IdUsuario, @Cargo)";
            var idFuncionario = Guid.NewGuid();
            var funcionarioRows = connection.Execute(queryFuncionario, new
            {
                IdFuncionario = idFuncionario,
                IdUsuario = usuario.IdUsuario,
                Cargo = cargo.ToString()
            }, transaction);

            if (userRows == 0 || funcionarioRows == 0)
            {
                transaction.Rollback();
                return (false, Guid.Empty, Guid.Empty);
            }

            transaction.Commit();
            return (true, usuario.IdUsuario, idFuncionario);
        }

        public (bool, Guid) CriarGestor(Guid idUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO [dbo].[Manager] (idGestor, idUsuario)
                            VALUES (@IdGestor, @IdUsuario)";
            var parameters = new
            {
                IdGestor = Guid.NewGuid(),
                IdUsuario = idUsuario
            };
            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, parameters.IdGestor);
            }
            return (false, Guid.Empty);
        }

        public List<dynamic> ListarAlunos(Guid idAcademia)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT a.idAluno, u.idAcademia,u.idUsuario, u.tipoUsuario, a.objetivo, u.nome, u.email   
                            FROM [dbo].[Member] a
                            JOIN [dbo].[User] u ON a.idUsuario = u.idUsuario
                            WHERE u.idAcademia = @IdAcademia";
            var alunos = connection.Query<dynamic>(query, new { IdAcademia = idAcademia }).ToList();
            return alunos;
        }

        public List<dynamic> ListarUsuarios(Guid idAcademia)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT idUsuario, idAcademia, nome, email, tipoUsuario, ativo
                            FROM [dbo].[User]
                            WHERE idAcademia = @IdAcademia";
            var usuarios = connection.Query<dynamic>(query, new { IdAcademia = idAcademia }).ToList();
            return usuarios;
        }

         public dynamic ObterGestor(Guid idGestor)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT idUsuario, idGestor, idAcademia, nome, email, CPF           
                            FROM [dbo].[User] u
                            JOIN [dbo].[Manager] g ON u.idUsuario = g.idUsuario
                            WHERE g.idGestor = @IdGestor";
            var result = connection.QueryFirstOrDefault<dynamic>(query, new { IdGestor = idGestor });
            if (result != null)
            {
                return result;
            }
            throw new Exception("Gestor não encontrado");
        }

        public (dynamic, int) ListarUsuario(Guid idusuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT idUsuario, idAcademia, nome, email, dataNascimento, CPF, tipoUsuario, ativo, quadra, rua, bairro, cidade, estado, cep
                            FROM [dbo].[User]
                            WHERE idUsuario = @IdUsuario";
        
            var result = connection.QueryFirstOrDefault<dynamic>(query, new { IdUsuario = idusuario });
            if (result == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            int tipoUsuario = result.tipoUsuario;
            switch (tipoUsuario)
            {
                case 1:
                    string queryAluno = @"SELECT idAluno, idInstrutor, idContrato, objetivo
                                        FROM [dbo].[Member]
                                        WHERE idUsuario = @IdUsuario";
                    var aluno = connection.QueryFirstOrDefault<dynamic>(queryAluno, new { IdUsuario = idusuario });
                    if (aluno != null)
                    {
                        result.idAluno = aluno.idAluno;
                        result.idInstrutor = aluno.idInstrutor;
                        result.idContrato = aluno.idContrato;
                        result.objetivo = aluno.objetivo;
                    }
                    
                    break;
                case 2:
                    string queryInstrutor = @"SELECT idInstrutor, cref
                                            FROM [dbo].[Instructor]
                                            WHERE idUsuario = @IdUsuario";
                    var instrutor = connection.QueryFirstOrDefault<dynamic>(queryInstrutor, new { IdUsuario = idusuario });
                    if (instrutor != null)
                    {
                        result.idInstrutor = instrutor.idInstrutor;
                        result.cref = instrutor.cref;
                    }
                    break;
                case 3:
                    string queryFuncionario = @"SELECT idFuncionario, cargo
                                            FROM [dbo].[Employee]
                                            WHERE idUsuario = @IdUsuario";
                    var funcionario = connection.QueryFirstOrDefault<dynamic>(queryFuncionario, new { IdUsuario = idusuario });
                    if (funcionario != null)
                    {
                        result.idFuncionario = funcionario.idFuncionario;
                        result.cargo = funcionario.cargo;
                    }
                    break;
                case 4:
                    string queryGestor = @"SELECT idGestor
                                            FROM [dbo].[Manager]
                                            WHERE idUsuario = @IdUsuario";
                    var gestor = connection.QueryFirstOrDefault<dynamic>(queryGestor, new { IdUsuario = idusuario });
                    if (gestor != null)
                    {
                        result.idGestor = gestor.idGestor;
                    }
                    break;
                default:
                    throw new Exception("Tipo de usuário inválido");
            }
            return (result, tipoUsuario);
        }

        public dynamic Ativar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic AtualizarFilho(Guid id, dynamic var, string atributo, UserType tipoUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo)
        {
            throw new NotImplementedException();
        }

        public dynamic Desativar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterAcademiaGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }

    }
}