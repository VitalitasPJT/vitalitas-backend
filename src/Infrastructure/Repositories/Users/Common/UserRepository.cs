using Domain.Features.Users.Common.Entities;
using Domain.Features.Shared.Entities;
using Domain.Features.Users.Common.Interfaces;
using Domain.ValueObjects;
using Dapper;
using Infrastructure.Database.Connections;
using Infrastructure.Records;
using Domain.Enums;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Infrastructure.Repositories.Users.Common
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public User Login(string email, string senha)
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
                    FROM [dbo].[User]
                    WHERE Email = @Email AND Senha = @Senha";

            string emailString = email.ToString();

            var record = connection.QueryFirstOrDefault<UserDto>(query, new { Email = email, Senha = senha });

            if (record == null)
            {
                return null;
            }

            var dataNasc = DateOnly.FromDateTime(record.DataNascimento);

            var usuarioEncontrado = new User(
                idUsuario: record.IdUsuario,
                idAcademia: record.IdAcademia,
                nome: record.Nome, 
                email: new Email(record.Email), 
                senha: record.Senha,
                dataNascimento: dataNasc,
                cpf: new CPF(record.Cpf), 
                tipoUsuario: (UserType)record.TipoUsuario, 
                ativo: true,
                flag: record.Flag,
                quadra: record.Quadra,
                rua: record.Rua,
                bairro: record.Bairro,
                cidade: record.Cidade,
                estado: record.Estado,
                cep: record.Cep
            );

            return usuarioEncontrado;
        }

        public Domain.Features.Shared.Entities.ActivityLog RegistrarAcao(Guid idusuario, int acao, string dispositivoLogado, string localizacao)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = @"INSERT INTO [dbo].[ActivityLog] 
            (IdLog, IdUsuario, DataHora, Acao, DispositivoLogado, Localizacao) 
            VALUES 
            (@IdLog, @IdUsuario, @DataHora, @Acao, @DispositivoLogado, @Localizacao);";

            var logAtividade = new Domain.Features.Shared.Entities.ActivityLog(idusuario, (LogAction)acao, dispositivoLogado, localizacao);

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
        public UserType? GetTipoUsuario(Guid idUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();

            string query = "SELECT TipoUsuario FROM [dbo].[User] WHERE IdUsuario = @IdUsuario";

            var tipoUsuario = connection.QueryFirstOrDefault<int?>(query, new { IdUsuario = idUsuario });

            if (tipoUsuario == null) return null;

            return (UserType)tipoUsuario.Value;
        }

        public Guid GetIdAcademia(Guid idUsuario) {
            using var connection = _connectionFactory.CreateConnection();
            string query = "SELECT IdAcademia FROM [dbo].[User] WHERE IdUsuario = @IdUsuario";
            var idAcademia = connection.QueryFirstOrDefault<Guid?>(query, new { IdUsuario = idUsuario });

            if (idAcademia == null)
                throw new Exception($"Academia não encontrada para o usuário {idUsuario}");
            return idAcademia.Value;
        }

        internal record UserDto
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


        public bool TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();

            string querySelect = "SELECT Senha FROM [dbo].[User] WHERE IdUsuario = @IdUsuario";
            string senhaSalva = connection.QueryFirstOrDefault<string>(querySelect, new { IdUsuario = idusuario });

            if (senhaSalva == novasenha)
            {
                throw new Exception("A nova senha não pode ser igual à senha atual.");
            }

            string queryUpdate = @"UPDATE [dbo].[User] SET Senha = @NovaSenha, flag = @Flag WHERE IdUsuario = @IdUsuario";

            var linhasAfetadas = connection.Execute(queryUpdate, new { NovaSenha = novasenha, Flag = false, IdUsuario = idusuario });
            
            return linhasAfetadas > 0;
        }
    }
}