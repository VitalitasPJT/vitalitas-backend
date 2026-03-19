using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
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
        public List<TelefoneUsuario> GetTelefonesByUsuarioId(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario GetUsuarioById(Guid idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}