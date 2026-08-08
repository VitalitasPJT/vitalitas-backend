using System;
using Domain.Enums;

namespace Application.Features.Users.Employee.Constructor
{
    public class ConstructorEmployee
    {
        public Guid IdUsuario { get; private set; }
        public Role Cargo { get; private set; }

        public ConstructorEmployee(Guid idUsuario, Role cargo)
        {
            IdUsuario = idUsuario;
            Cargo = cargo;
        }
    }
}