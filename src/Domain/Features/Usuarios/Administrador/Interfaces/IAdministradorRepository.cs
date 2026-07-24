using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Usuarios.Administrador.Interfaces
{
    public interface IAdministradorRepository
    {
        List<Domain.Features.Usuarios.Administrador.Entities.Administrador> ListarAdministradores(Guid idacademia);
        Guid CriarAdministrador(Domain.Features.Usuarios.Administrador.Entities.Administrador administrador);
    }
}
