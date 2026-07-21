using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Features.Usuarios.Gestor.Entities
{
    public class Gestor
    {
        public Guid IdGestor { get; private set; }
        public Guid IdUsuario { get; private set; }

        public Gestor(Guid idUsuario)
        {
            IdGestor = Guid.NewGuid();
            IdUsuario = idUsuario;
        }
        public Gestor() { } 
    }
}