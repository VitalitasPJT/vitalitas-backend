using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class XpHistorico
    {
        public Guid idXp {  get; private set; }
        public int XpGanho { get; private set; }
        public DateTime Data {  get; private set; }
        public string Motivo { get; private set; }
    }
}
