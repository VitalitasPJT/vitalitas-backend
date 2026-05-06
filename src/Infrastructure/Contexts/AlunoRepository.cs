using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vitalitas.Infrastructure.Database.Connection;

namespace Infrastructure.Persistence
{
    public class AlunoRepository : IAluno
    {
        private readonly DbConnectionFactory _connectionFactory;
        public  AlunoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public dynamic AtualizarObjetivo(Guid idaluno, string novoobjetivo)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = "UPDATE aluno SET objetivo = @novoobjetivo WHERE idAluno = @idaluno";
            var parameters = new { idaluno, novoobjetivo };

            connection.Execute(query, parameters);

            return new { Success = true, Message = "Objetivo atualizado com sucesso." };
        }

        public Guid CriarAluno(Guid idInstrutor, Guid idUsuario, int idContrato, Guid idAcademia, string objetivo)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = "INSERT INTO aluno (idAluno, idAcademia, idUsuario, objetivo) VALUES (@IdAluno, @IdAcademia, @IdUsuario, @Objetivo)";
            var parameters = new
            {
                IdAluno = Guid.NewGuid(),
                //IdInstrutor = idInstrutor,
                IdContrato = idContrato,
                IdAcademia = idAcademia,
                IdUsuario = idUsuario,
                Objetivo = objetivo
            };

            connection.Execute(query, parameters);
            return parameters.IdAluno;
        }

        public dynamic ListarAluno(Guid aluno)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = @"
                SELECT a.idAluno, a.idAcademia, a.idUsuario, a.objetivo,
                       u.nome, u.email, u.tipoUsuario
                FROM aluno a
                INNER JOIN usuario u ON a.idUsuario = u.idUsuario
                WHERE a.idAluno = @idaluno";

            var parameters = new { idaluno = aluno };

            var result = connection.QueryFirstOrDefault(query, parameters);

            if (result == null)
               throw new Exception("Aluno não encontrado.");

            return result;
        }

        public dynamic ListarAluno(Guid idAcademia, Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> ListarALunos(Guid idacademia)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = @"
                SELECT a.Id, a.IdAcademia, a.IdUsuario, a.Objetivo,
                       u.Nome, u.Email, u.TipoUsuario
                FROM Alunos a
                INNER JOIN Usuarios u ON a.IdUsuario = u.Id
                WHERE a.IdAcademia = @idacademia";

            var parameters = new { idacademia };

            var results = connection.Query(query, parameters).ToList();

            dynamic alunos = new List<dynamic>();

            foreach (var result in results)
            {
                alunos.Add(new
                {
                    Id = result.Id,
                    IdAcademia = result.IdAcademia,
                    IdUsuario = result.IdUsuario,
                    Objetivo = result.Objetivo,
                    Email = result.Email,
                    Nome = result.Nome
                });

            }

            return alunos;
        }

        public dynamic TrocarSenha(Guid idUsuario, string novaSenha)
        {
            throw new NotImplementedException();
        }

        public dynamic VincularInstrutor(Guid idaluno, Guid idprofessor)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = "UPDATE Alunos SET IdProfessor = @idprofessor WHERE Id = @idaluno";
            var parameters = new { idaluno, idprofessor };

            connection.Execute(query, parameters);

            return new { Success = true, Message = "Instrutor vinculado com sucesso." };
        }
    }
}
