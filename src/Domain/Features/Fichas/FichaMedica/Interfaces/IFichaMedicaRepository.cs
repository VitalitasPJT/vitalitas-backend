using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Fichas.FichaMedica.Interfaces
{
    public interface IFichaMedicaRepository
    {
        (bool, Guid) CriarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica fichaMedica);
        dynamic AtualizarFichaMedica(Domain.Features.Fichas.FichaMedica.Entities.FichaMedica novaFichaMedica);
        Domain.Features.Fichas.FichaMedica.Entities.FichaMedica ListarFichaMedica(Guid idAluno);
    }
}

