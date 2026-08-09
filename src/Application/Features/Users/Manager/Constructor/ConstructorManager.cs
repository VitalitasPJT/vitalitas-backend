using System;

namespace Application.Features.Users.Manager.Constructor
{
    public class ConstructorManager
    {
        public Guid IdUsuario { get; private set; }

        public ConstructorManager(Guid idUsuario)
        {
            IdUsuario = idUsuario;
        }
    }
}