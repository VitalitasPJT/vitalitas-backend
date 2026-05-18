using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Identity.Client;
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

        public bool AtualizarObjetivo(Guid idaluno, string novoobjetivo)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = "UPDATE aluno SET objetivo = @novoobjetivo WHERE idAluno = @idaluno";
            var parameters = new { idaluno, novoobjetivo };

            var rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected == 0)
            {
                throw new Exception("Aluno não encontrado ou objetivo já é o mesmo.");
            }

            return rowsAffected > 0;
        }

        public dynamic ListarAluno(Guid aluno)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = @"
                SELECT a.idAluno, u.idAcademia, a.idUsuario, a.objetivo,
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


        /*public List<dynamic> ListarALunos(Guid idacademia)
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
        }*/

        public bool TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();
            
            string querySelect = "SELECT Senha FROM Usuario WHERE IdUsuario = @IdUsuario";
            string senhaSalva = connection.QueryFirstOrDefault<string>(querySelect, new { IdUsuario = idusuario });

            if (senhaSalva == novasenha)
            {
                throw new Exception("A nova senha não pode ser igual à senha atual.");
            }

            string query = @"UPDATE Usuario SET Senha = @NovaSenha, flag = @Flag WHERE IdUsuario = @IdUsuario";

            var linhasAfetadas = connection.Execute(query, new { NovaSenha = novasenha, Flag = false, IdUsuario = idusuario });
            return linhasAfetadas > 0;
        }

        public bool VincularInstrutor(Guid idaluno, Guid idprofessor)
        {
            using var connection = _connectionFactory.CreateConnection();

            var query = "UPDATE aluno SET IdInstrutor = @idprofessor WHERE IdAluno = @idaluno";
            var parameters = new { idaluno, idprofessor };

            var rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected == 0)
            {
                throw new Exception("Aluno ou instrutor não encontrado, ou já estão vinculados.");
            }

            return rowsAffected > 0;
        }

        public Guid? ObterIdUsuarioPorAluno(Guid idAluno)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = "SELECT idUsuario FROM aluno WHERE idAluno = @IdAluno";
            return connection.QueryFirstOrDefault<Guid?>(query, new { IdAluno = idAluno });
        }
    }
}
