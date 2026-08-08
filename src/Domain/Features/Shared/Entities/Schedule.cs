using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Shared.Entities
{
    public class Schedule
    {
        public int IdAgenda { get; private set; }
        public Guid IdInstrutor { get; private set; }
        public Guid IdAcademia { get; private set; }
        public ScheduleStatus Status { get; private set; }
        public DateTime Data { get; private set; }

        public Schedule(Guid idInstrutor, Guid idAcademia, DateTime data)
        {
            IdAgenda = 0; 
            IdInstrutor = idInstrutor;
            IdAcademia = idAcademia;
            Data = data;
            Status = ScheduleStatus.Agendado;
        }

        public Schedule() { }
    }
}
