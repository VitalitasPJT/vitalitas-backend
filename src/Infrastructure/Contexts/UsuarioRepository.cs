using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Dapper;
using Vitalitas.Infrastructure.Database.Connection;
using Infrastructure.Records;
using Domain.Enums;

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

            // A query limpa, focada na sua classe UsuarioDB
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

            // 1. O Dapper usa a sua nova classe UsuarioDB para mapear os dados do banco
            var record = connection.QueryFirstOrDefault<UsuarioDB>(query, new { Email = emailString, Senha = senha });

            // 2. Se não encontrou o usuário, retorna nulo para o UseCase tratar
            if (record == null)
            {
                return null;
            }

            // 3. Monta a Entidade Rica de Domínio
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
            connection.Open();
            string query = @"UPDATE Usuario SET Senha = @NovaSenha WHERE IdUsuario = @IdUsuario";
            
            var record = connection.Execute(query, new { NovaSenha = novasenha, IdUsuario = idusuario });
            return record;
        }
    }
}