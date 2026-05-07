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

        public Usuario Login(string email, string senha)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @"SELECT 
                        idUsuario as IdUsuario, 
                        idAcademia as IdAcademia,
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

            var record = connection.QueryFirstOrDefault<UsuarioDto>(query, new { Email = email, Senha = senha });

            if (record == null)
            {
                return null;
            }

            var dataNasc = DateOnly.FromDateTime(record.DataNascimento);

            var usuarioEncontrado = new Usuario(
                idUsuario: record.IdUsuario,
                idAcademia: record.IdAcademia,
                nome: record.Nome, 
                email: new Email(record.Email), 
                quadra: record.Quadra,
                rua: record.Rua,
                bairro: record.Bairro,
                cidade: record.Cidade,
                estado: record.Estado,
                cep: record.Cep,
                senha: record.Senha,
                dataNascimento: dataNasc,
                cpf: new CPF(record.Cpf), 
                tipoUsuario: (TipoUsuario)record.TipoUsuario, 
                flag: record.Flag
            );

            return usuarioEncontrado;
        }

        public LogAtividade RegistrarAcao(Guid idusuario, int acao, string dispositivoLogado, string localizacao)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @"INSERT INTO LogAtividade 
            (IdLog, IdUsuario, DataHora, Acao, DispositivoLogado, Localizacao) 
            VALUES 
            (@IdLog, @IdUsuario, @DataHora, @Acao, @DispositivoLogado, @Localizacao);";

            var logAtividade = new LogAtividade(idusuario, (AcaoLog)acao, dispositivoLogado, localizacao);

            connection.Execute(query, new
            {
                logAtividade.IdLog,
                logAtividade.IdUsuario,
                logAtividade.DataHora,
                logAtividade.Acao,
                logAtividade.DispositivoLogado,
                logAtividade.Localizacao
            });

            return logAtividade;
        }
        public TipoUsuario? GetTipoUsuario(Guid idUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = "SELECT TipoUsuario FROM Usuario WHERE IdUsuario = @IdUsuario";

            var tipoUsuario = connection.QueryFirstOrDefault<int?>(query, new { IdUsuario = idUsuario });

            if (tipoUsuario == null) return null;

            return (TipoUsuario)tipoUsuario.Value;
        }

        internal record UsuarioDto
        (
            Guid IdUsuario,
            Guid IdAcademia,
            string Nome,
            string Email,
            string Quadra,
            string Rua,
            string Bairro,
            string Cidade,
            string Estado,
            string Cep,
            string Senha,
            DateTime DataNascimento,
            string Cpf,
            int TipoUsuario,
            bool Flag
        );


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
        }

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

        public dynamic TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"UPDATE Usuario SET Senha = @NovaSenha, flag = @Flag WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { NovaSenha = novasenha, Flag = false, IdUsuario = idusuario });
            return record;
        }*/





    }
}