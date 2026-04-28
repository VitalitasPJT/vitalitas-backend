using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Dapper;
using Vitalitas.Infrastructure.Database.Connection;
using Infrastructure.Records;
using Domain.Enums;
using System.Data;

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
            throw new NotImplementedException();
        }

        public dynamic Ativar(Guid idusuario)
        {
            throw new NotImplementedException();
        }

        public dynamic AtualizarDados(Guid idusuario, dynamic var, string atributo)
        {
            throw new NotImplementedException();
        }

        public Guid CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

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

        public dynamic Desativar(Guid idusuario)
        {
            throw new NotImplementedException();
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

        public List<dynamic> ObterLogs(Guid idusuario)
        {
            throw new NotImplementedException();
        }

        public dynamic TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            string query = @"UPDATE Usuario SET Senha = @NovaSenha, flag = @Flag WHERE IdUsuario = @IdUsuario";

            var record = connection.Execute(query, new { NovaSenha = novasenha, Flag = false, IdUsuario = idusuario });
            return record;
        }
    }
}