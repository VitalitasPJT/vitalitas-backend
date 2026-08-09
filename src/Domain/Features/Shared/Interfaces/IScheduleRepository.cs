using Domain.Features.Shared.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Interfaces
{
    public interface IScheduleRepository
    {
        Guid Agendar(Guid idInstrutor, Guid idAcademia, DateOnly data);
        dynamic AtualizarEvento(Guid idAgenda, DateTime novaData, ScheduleStatus novoStatus);
        dynamic RemoverEvento(Guid idAgenda);
        List<Schedule> ListarEventos(Guid idAcademia);
        List<Schedule> ListarEventosPorData(DateOnly data, Guid idAcademia);
        List<Schedule> ListarEventosPorInstrutor(Guid idInstrutor);
        List<Schedule> ListarEventosPorPeriodo(DateOnly dataInicio, DateOnly dataFim, Guid idAcademia);
        Schedule BuscarEventoPorId(Guid idAgenda);
        bool VerificarConflito(Guid idInstrutor, DateTime data);
        bool EstaDisonivel(Guid idInstrutor, DateTime data);
    }
}
