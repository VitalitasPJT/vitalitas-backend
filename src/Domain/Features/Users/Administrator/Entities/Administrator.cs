using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Users.Administrator.Entities
{
    public class Administrator
    {
        public Guid IdFuncionario { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Role Cargo { get; private set; }
        public Administrator(Guid idFuncionario, Guid idUsuario, Role cargo)
        {
            IdFuncionario = idFuncionario;
            IdUsuario = idUsuario;
            Cargo = cargo;
        }
        public Administrator() {}
    }
    
}