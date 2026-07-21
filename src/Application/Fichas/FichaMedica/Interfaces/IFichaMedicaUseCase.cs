using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Application.Fichas.FichaMedica.Response.FichaMedicaRP;
using static Application.Compartilhado.Constructor;

namespace Application.Fichas.FichaMedica.Interfaces
{
    public interface IFichaMedicaUseCase
    {
        CriarFichaMedicaResponse CriarFichaMedica(ConstructorFichaMedica fichaMedica);
    }
}