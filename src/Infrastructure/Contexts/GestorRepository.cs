using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Vitalitas.Infrastructure.Database.Connection;

namespace Infrastructure.Contexts
{
    public class GestorRepository : IGestorRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public GestorRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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

        public (bool, Guid) CriarAluno(Aluno aluno)
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

        public dynamic CriarFuncionario(Guid idUsuario, Cargo cargo)
        {
            throw new NotImplementedException();
        }

        public dynamic CriarGestor(Guid idUsuario)
        {
            throw new NotImplementedException();
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

        public dynamic Desativar(Guid idUsuario)
        {
            throw new NotImplementedException();
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

        public Usuario ListarUsuario(Guid idusuario)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> ListarUsuarios(Guid idAcademia)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterAcademiaGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }
    }
}