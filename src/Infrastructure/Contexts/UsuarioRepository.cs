using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Dapper;
using Vitalitas.Infrastructure.Database.Connection;

namespace Infrastructure.Persistence
{
    public class UsuarioRepository : IUsuario
    {
        private readonly DbConnectionFactory _connectionFactory;
        public UsuarioRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Usuario Login(Email email, string senha)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            string query = @"SELECT 
                    Id_usuario as IdUsuario, 
                    Nome
                    Email,
                    Endereco as Endereço,
                    Senha,
                    DataNascimento,
                    CPF,
                    TipoUsuario,
                    Flag
                FROM Usuario
                WHERE Email = @Email AND @Senha = Senha";
            var usuarioEncontrado = connection.QueryFirstOrDefault<Usuario>(query, new { oEmail = email.ToString(), Senha = senha });

            return usuarioEncontrado;
        }

        public void TrocarSenha(Guid idusuario, string novasenha)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            // Implementar a lógica de troca de senha aqui, utilizando o ID do usuário e a nova senha fornecida.
            
        }
    }
}