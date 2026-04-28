using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Dapper;
using Vitalitas.Infrastructure.Database.Connection;
using Infrastructure.Records;
using Domain.Enums;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.Persistence
{
    public class UsuarioRepository : IUsuario
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UsuarioRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public dynamic AdicionarLog(Guid idusuario, LogAtividade log)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @"INSERT INTO LogAtividade 
            (IdLog, IdUsuario, Acao, DataHora) 
            VALUES 
            (@IdLog, @IdUsuario, @Acao, @DataHora);";

            var record = connection.Execute(query, new
            {
                IdLog = Guid.NewGuid(),
                IdUsuario = idusuario,
                Acao = log.Acao, 
                DataHora = DateTime.Now 
            });

            return record;
        }

        public List<dynamic> ObterLogs(Guid idusuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = "SELECT * FROM LogAtividade WHERE IdUsuario = @IdUsuario ORDER BY DataHora DESC";

            var logs = connection.Query<dynamic>(query, new { IdUsuario = idusuario }).ToList();

            return logs;
        }

        /*public dynamic Ativar(Guid idusuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = "UPDATE Usuario SET Flag = @Flag WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { Flag = true, IdUsuario = idusuario });

            return record;
        }*/

        /*public dynamic Desativar(Guid idusuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = "UPDATE Usuario SET Flag = @Flag WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { Flag = false, IdUsuario = idusuario });

            return record;
        }*/

        public dynamic AtualizarDados(Guid idusuario, dynamic var, string atributo)
        {
            var colunasPermitidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Nome", "Email", "Quadra", "Rua", "Bairro", "Cidade",
                "Estado", "Cep", "DataNascimento", "Cpf", "TipoUsuario"
            };

            if (!colunasPermitidas.Contains(atributo))
            {
                throw new ArgumentException($"O atributo '{atributo}' não é válido ou não tem permissão para ser atualizado dinamicamente.");
            }

            using var connection = _connectionFactory.CreateConnection();

            string query = $"UPDATE Usuario SET {atributo} = @Valor WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { Valor = var, IdUsuario = idusuario });

            return record;
        }

        public Guid CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            var id = Guid.NewGuid();

            string query = @"INSERT INTO Usuario 
            (IdUsuario, Nome, Email, Senha, Quadra, Rua, Bairro, Cidade, Estado, Cep, DataNascimento, Cpf, TipoUsuario, Flag) 
            VALUES 
            (@IdUsuario, @Nome, @Email, @Senha, @Quadra, @Rua, @Bairro, @Cidade, @Estado, @Cep, @DataNascimento, @Cpf, @TipoUsuario, @Flag);";

            connection.Execute(query, new
            {
                IdUsuario = id,
                Nome = nome,
                Email = email,
                Senha = senha,
                Quadra = quadra,
                Rua = rua,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Cep = cep,
                DataNascimento = dataNascimento.ToDateTime(TimeOnly.MinValue),
                Cpf = cpf,
                TipoUsuario = (int)tipoUsuario,
                Flag = true
            });

            return id;
        }

        public Usuario Login(string email, string senha)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @"SELECT 
                    idUsuario as IdUsuario, 
                    nome as Nome,
                    email as Email,
                    quadra as Quadra,
                    rua as Rua,
                    bairro as Bairro,
                    cidade as Cidade,
                    estado as Estado,
                    cep as Cep,
                    senha as Senha,
                    dataNascimento as DataNascimento,
                    cpf as Cpf,
                    tipoUsuario as TipoUsuario,
                    flag as Flag
                FROM Usuario
                WHERE Email = @Email AND Senha = @Senha";

            string emailString = email.ToString();

            var record = connection.QueryFirstOrDefault<UsuarioDB>(query, new { Email = emailString, Senha = senha });

            if (record == null)
            {
                return null;
            }

            var usuarioEncontrado = new Usuario(
                idUsuario: record.IdUsuario,
                nome: new Nome(record.Nome),
                email: new Email(record.Email),
                quadra: record.Quadra,
                rua: record.Rua,
                bairro: record.Bairro,
                cidade: record.Cidade,
                estado: record.Estado,
                cep: record.Cep,
                senha: record.Senha,
                dataNascimento: DateOnly.FromDateTime(record.DataNascimento),
                cpf: new CPF(record.Cpf),
                tipoUsuario: (TipoUsuario)record.TipoUsuario,
                flag: record.Flag
            );

            return usuarioEncontrado;
        }

        public dynamic TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"UPDATE Usuario SET Senha = @NovaSenha, flag = @Flag WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { NovaSenha = novasenha, Flag = false, IdUsuario = idusuario });
            return record;
        }
    }
}