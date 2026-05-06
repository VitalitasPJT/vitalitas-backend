using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class LogAtividade
    {
        public Guid IdLog { get; private set; }
        public DateTime DataHora { get; private set; }
        public AcaoLog Acao { get; private set; }
    }
}