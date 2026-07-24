using System;

namespace Application.Usuarios.Gestor.Constructor
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