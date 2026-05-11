using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFichaMedicaRepository
    {
        (bool, Guid) CriarFichaMedica(FichaMedica fichaMedica);
        dynamic AtualizarFichaMedica(FichaMedica novaFichaMedica);
        FichaMedica ListarFichaMedica(Guid idAluno);
    }
}

