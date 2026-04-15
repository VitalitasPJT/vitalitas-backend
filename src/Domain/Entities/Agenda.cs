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
        public Guid IdAgenda { get; private set;  }
        public StatusAgenda Status {  get; private set; }
        public DateOnly Data { get; private set; }
    }
}
