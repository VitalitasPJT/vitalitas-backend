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
        Guid CriarFichaMedica(Guid idAluno, string alergia, string restricao, string lesao, string cirurgia, string problemaSaude, string usoMedicamento);
        dynamic AtualizarFichaMedica(FichaMedica novaFichaMedica);
        FichaMedica ListarFichaMedica(Guid idAluno);
    }
}

