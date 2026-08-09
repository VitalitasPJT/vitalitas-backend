using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Users.Member.Entities
{
    public class XpHistory
    {
        public Guid IdXp {  get; private set; }
        public Guid IdAcademia { get; private set; }
        public Guid IdUsuario { get; private set; }
        public int XpGanho { get; private set; }
        public DateTime Data {  get; private set; }
        public string Motivo { get; private set; }

        public XpHistory(Guid idUsuario, int xpGanho, string motivo)
        {
            IdXp = Guid.NewGuid();
            IdUsuario = idUsuario;
            XpGanho = xpGanho;
            Data = DateTime.UtcNow;
            Motivo = motivo;
        }

        public XpHistory() {}
    }
}
