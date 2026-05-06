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
        Guid CriarFichaMedica(FichaMedica fichamedica);
        dynamic AtualizarFichaMedica(FichaMedica novafichamedica);
        FichaMedica ListarFichaMedica(Guid idaluno);
    }
}

