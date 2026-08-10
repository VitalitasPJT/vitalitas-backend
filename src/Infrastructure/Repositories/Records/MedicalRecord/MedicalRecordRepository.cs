using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Features.Records.MedicalRecord.Interfaces;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.Records.MedicalRecord
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public MedicalRecordRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public dynamic AtualizarFichaMedica(Domain.Features.Records.MedicalRecord.Entities.MedicalRecord novaFichaMedica)
        {
            throw new NotImplementedException();
        }

        public (bool, Guid) CriarFichaMedica(Domain.Features.Records.MedicalRecord.Entities.MedicalRecord fichaMedica)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO [dbo].[] (idFicha, idAluno, alergia, restricao, lesao, cirurgia, problemaSaude, usoMedicamento)
                            VALUES (@IdFicha, @IdAluno, @Alergia, @Restricao, @Lesao, @Cirurgia, @ProblemaSaude, @UsoMedicamento)";
            var parameters = new
            {
                fichaMedica.IdFicha,
                fichaMedica.IdAluno,
                fichaMedica.Alergia,
                fichaMedica.Restricao,
                fichaMedica.Lesao,
                fichaMedica.Cirurgia,
                fichaMedica.ProblemaSaude,
                fichaMedica.UsoMedicamento
            };
            int rowsAffected = connection.Execute(query, parameters);
            if (rowsAffected > 0)
            {
                return (true, parameters.IdFicha);
            }
            return (false, Guid.Empty);
        }

        public Domain.Features.Records.MedicalRecord.Entities.MedicalRecord ListarFichaMedica(Guid idAluno)
        {
            throw new NotImplementedException();
        }
    }
}