using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Features.Users.Manager.Entities
{
    public class Manager
    {
        public Guid IdGestor { get; private set; }
        public Guid IdUsuario { get; private set; }

        public Manager(Guid idUsuario)
        {
            IdGestor = Guid.NewGuid();
            IdUsuario = idUsuario;
        }
        public Manager() { } 
    }
}