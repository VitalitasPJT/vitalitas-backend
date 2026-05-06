using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgendaRepository
    {
        Guid Agendar(Agenda agenda);
        dynamic Cancelar(Guid idagenda);
        dynamic Concluir(Guid idagenda);
        dynamic Reagendar(Guid idagenda, DateTime novadata);
    }
}
