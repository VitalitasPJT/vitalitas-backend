using Domain.Features.Shared.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Interfaces
{
    public interface IAgendaRepository
    {
        Guid Agendar(Guid idInstrutor, Guid idAcademia, DateOnly data);
        dynamic AtualizarEvento(Guid idAgenda, DateTime novaData, StatusAgenda novoStatus);
        dynamic RemoverEvento(Guid idAgenda);
        List<Agenda> ListarEventos(Guid idAcademia);
        List<Agenda> ListarEventosPorData(DateOnly data, Guid idAcademia);
        List<Agenda> ListarEventosPorInstrutor(Guid idInstrutor);
        List<Agenda> ListarEventosPorPeriodo(DateOnly dataInicio, DateOnly dataFim, Guid idAcademia);
        Agenda BuscarEventoPorId(Guid idAgenda);
        bool VerificarConflito(Guid idInstrutor, DateTime data);
        bool EstaDisonivel(Guid idInstrutor, DateTime data);
    }
}
