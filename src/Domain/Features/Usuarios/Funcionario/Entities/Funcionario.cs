using Domain.Enums;

namespace Domain.Features.Usuarios.Funcionario.Entities
{
    public class Funcionario
    {
        public Guid IdFuncionario { get; private set; }
        public Guid IdUsuario { get; private set; }
        public Cargo Cargo { get; private set; }
        public Funcionario(Guid idFuncionario, Guid idUsuario, Cargo cargo)
        {
            IdFuncionario = idFuncionario;
            IdUsuario = idUsuario;
            Cargo = cargo;
        }

        public Funcionario() {}
    }
    
}