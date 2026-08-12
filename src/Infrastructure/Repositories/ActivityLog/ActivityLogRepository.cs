using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Enums;
using Domain.Features.Shared.Interfaces;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.ActivityLog
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public ActivityLogRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public List<dynamic> ObterLogsPorAcademia(Guid idAcademia)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"SELECT al.idLog, al.dataHora, al.acao, al.dispositivoLogado, u.nome, u.email
                            FROM [dbo].[ActivityLog] al
                            INNER JOIN [dbo].[User] u ON al.idUsuario = u.idUsuario
                            WHERE u.idAcademia = @IdAcademia
                            ORDER BY al.dataHora DESC";
            var logs = connection.Query<dynamic>(query, new { IdAcademia = idAcademia }).ToList();
            return logs;
        }

        public Task RegistrarAtividadeAsync(Guid idUsuario, DateTime dataHora, LogAction acao, string dispositivoLogado, string localizacao)
        {
            throw new NotImplementedException();
        }
    }
}