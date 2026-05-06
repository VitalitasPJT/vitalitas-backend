using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Agenda
    {
        public int IdAgenda { get; private set; }
        public Guid IdInstrutor { get; private set; }
        public Guid IdAcademia { get; private set; }
        public StatusAgenda Status { get; private set; }
        public DateTime Data { get; private set; }

        public Agenda(Guid idInstrutor, Guid idAcademia, DateTime data)
        {
            IdAgenda = 0; 
            IdInstrutor = idInstrutor;
            IdAcademia = idAcademia;
            Data = data;
            Status = StatusAgenda.Agendado;
        }

        public Agenda() { }
    }
}
