using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Instrutor
    {
        public Guid IdInstrutor { get; private set; }
        public Guid IdUsuario { get; private set; }
        public List<Guid> IdAgenda { get; private set; }
        public CREF CREF { get; private set; }
  
    }
    
}