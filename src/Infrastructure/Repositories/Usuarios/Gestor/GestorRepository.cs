using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Instrutor.Entities;
using Domain.Enums;
using Domain.Features.Usuarios.Gestor.Interfaces;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.Usuarios.Gestor
{
    public class GestorRepository : IGestorRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public GestorRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public (bool, Guid) CriarUsuario(Usuario usuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO Usuario (idUsuario, idAcademia, nome, email, senha, dataNascimento, cpf, tipoUsuario, ativo, flag, quadra, rua, bairro, cidade, estado, cep)
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

            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, usuario.IdUsuario);
            }
            return (false, Guid.Empty);
        }

        public (bool, Guid) CriarAluno(Domain.Features.Usuarios.Aluno.Entities.Aluno aluno)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO Aluno (idAluno, idInstrutor, idUsuario, idContrato, objetivo)
                            VALUES (@IdAluno, @IdInstrutor, @IdUsuario, @IdContrato, @Objetivo)";
            var parameters = new
            {
                aluno.IdAluno,
                aluno.IdInstrutor,
                aluno.IdUsuario,
                aluno.IdContrato,
                aluno.Objetivo
            };
            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, aluno.IdAluno);
            }
            return (false, Guid.Empty);
        }

        public (bool, Guid) CriarInstrutor(Instrutor instrutor)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO Instrutor (idInstrutor, idUsuario, cref)
                            VALUES (@IdInstrutor, @IdUsuario, @CREF)";
            var parameters = new
            {
                instrutor.IdInstrutor,
                instrutor.IdUsuario,
                CREF = instrutor.CREF.Valor
            };
            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, instrutor.IdInstrutor);
            }
            return (false, Guid.Empty);
        }

        public (bool, Guid) CriarFuncionario(Guid idUsuario, Cargo cargo)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO Funcionario (idFuncionario, idUsuario, cargo)
                            VALUES (@IdFuncionario, @IdUsuario, @Cargo)";
            var parameters = new
            {
                IdFuncionario = Guid.NewGuid(),
                IdUsuario = idUsuario,
                Cargo = cargo.ToString()
            };
            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, parameters.IdFuncionario);
            }
            return (false, Guid.Empty);
        }

        public (bool, Guid) CriarGestor(Guid idUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO Gestor (idGestor, idUsuario)
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
                            FROM aluno a
                            JOIN usuario u ON a.idUsuario = u.idUsuario
                            WHERE u.idAcademia = @IdAcademia";
            var alunos = connection.Query<dynamic>(query, new { IdAcademia = idAcademia }).ToList();
            return alunos;
        }

        public List<dynamic> ListarUsuarios(Guid idAcademia)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT idUsuario, idAcademia, nome, email, tipoUsuario, ativo
                            FROM Usuario
                            WHERE idAcademia = @IdAcademia";
            var usuarios = connection.Query<dynamic>(query, new { IdAcademia = idAcademia }).ToList();
            return usuarios;
        }

         public dynamic ObterGestor(Guid idGestor)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT idUsuario, idGestor, idAcademia, nome, email, CPF           
                            FROM Usuario u
                            JOIN Gestor g ON u.idUsuario = g.idUsuario
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
                            FROM Usuario
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
                                        FROM Aluno
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
                                            FROM Instrutor
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
                                            FROM Funcionario
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
                                            FROM Gestor
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

        public dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario)
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