using System;

namespace Application.Usuarios.Gestor.DTO
{
    public class ConstructorGestor
    {
        public Guid IdUsuario { get; private set; }

        public ConstructorGestor(Guid idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}