using System;
using Domain.Enums;

namespace Application.Usuarios.Funcionario.DTO
{
    public class ConstructorFuncionario
    {
        public Guid IdUsuario { get; private set; }
        public Cargo Cargo { get; private set; }

        public ConstructorFuncionario(Guid idUsuario, Cargo cargo)
        {
            IdUsuario = idUsuario;
            Cargo = cargo;
        }
    }
}