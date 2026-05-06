using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Administrador
    {
        public Guid IdFuncionario { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Cargo Cargo { get; private set; }
        public Administrador(Guid idFuncionario, Guid idUsuario, Cargo cargo)
        {
            IdFuncionario = idFuncionario;
            IdUsuario = idUsuario;
            Cargo = cargo;
        }
    }
    
}