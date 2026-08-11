using Dapper;
using Domain.Features.Token.Entities;
using Domain.Features.Token.Interfaces;
using Infrastructure.Records;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.Token
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public RefreshTokenRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Save(RefreshToken refreshToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"
                INSERT INTO [dbo].[RefreshToken] (idRefreshToken, tokenHash, dataExpiracao, revogado, idUsuario)
                VALUES (@IdRefreshToken, @TokenHash, @DataExpiracao, @Revogado, @IdUsuario)";

            connection.Execute(query, new
            {
                refreshToken.IdRefreshToken,
                refreshToken.TokenHash,
                refreshToken.DataExpiracao,
                refreshToken.Revogado,
                refreshToken.IdUsuario
            });
        }

        public RefreshToken? FindByTokenHash(string tokenHash)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"
                SELECT idRefreshToken AS IdRefreshToken,
                       tokenHash      AS TokenHash,
                       dataExpiracao  AS DataExpiracao,
                       revogado       AS Revogado,
                       idUsuario      AS UsuarioId
                FROM [dbo].[RefreshToken]
                WHERE tokenHash = @TokenHash";

            var record = connection.QueryFirstOrDefault<RefreshTokenDB>(query, new { TokenHash = tokenHash });
            if (record == null)
                return null;

            return new RefreshToken(
                record.IdRefreshToken,
                record.TokenHash,
                record.DataExpiracao,
                record.Revogado,
                record.UsuarioId
            );
        }

        public void Revoke(Guid idRefreshToken)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"
                UPDATE [dbo].[RefreshToken]
                SET revogado = 1
                WHERE idRefreshToken = @IdRefreshToken";

            connection.Execute(query, new { IdRefreshToken = idRefreshToken });
        }
    }
}
