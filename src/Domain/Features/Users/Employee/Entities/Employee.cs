using Domain.Enums;

namespace Domain.Features.Users.Employee.Entities
{
    public class Employee
    {
        public Guid IdFuncionario { get; private set; }
        public Guid IdAcademia { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Role Cargo { get; private set; }
        public Employee(Guid idFuncionario, Guid idUsuario, Role cargo)
        {
            IdFuncionario = idFuncionario;
            IdUsuario = idUsuario;
            Cargo = cargo;
        }

        public Employee() {}
    }
    
}