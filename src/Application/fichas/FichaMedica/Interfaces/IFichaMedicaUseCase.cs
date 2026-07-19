using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.DTOs.Response.FichaMedicaRP;
using static DTOs.Constructor.Constructor;

namespace Application.Interfaces
{
    public interface IFichaMedicaUseCase
    {
        CriarFichaMedicaResponse CriarFichaMedica(ConstructorFichaMedica fichaMedica);
    }
}