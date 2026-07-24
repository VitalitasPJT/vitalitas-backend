using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Features.Fichas.FichaMedica.Interfaces;
using Infrastructure.Database.Connections;

namespace Infrastructure.Repositories.Fichas.FichaMedica
{
    public class FichaMedicaRepository : IFichaMedicaRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public FichaMedicaRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public dynamic AtualizarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica novaFichaMedica)
        {
            throw new NotImplementedException();
        }

        public (bool, Guid) CriarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica fichaMedica)
        {
            using var connection = _connectionFactory.CreateConnection();
            string query = @"INSERT INTO FichaMedica (idFicha, idAluno, alergia, restricao, lesao, cirurgia, problemaSaude, usoMedicamento)
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

        public Domain.Features.Fichas.FichaMedica.Entities.FichaMedica ListarFichaMedica(Guid idAluno)
        {
            throw new NotImplementedException();
        }
    }
}