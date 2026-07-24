using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Database.Connections
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            // Ele vai buscar a string de conexão no seu appsettings.json da API
            _connectionString = _configuration.GetConnectionString("ConexaoPadrao") 
                                ?? throw new InvalidOperationException("String de conexão 'ConexaoPadrao' não encontrada.");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
  
    }
}