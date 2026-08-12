using System;
using Domain.Enums;

namespace Application.Features.ActivityLog.Constructor
{
    public class ConstructorActivityLogRecord
    {
        public Guid IdLog { get; private set; }
        public Guid IdUsuario { get; private set; }
        public DateTime DataHora { get; private set; }
        public LogAction Acao { get; private set; }
        public string DispositivoLogado { get; private set; }
        public string Localizacao { get; private set; }

        public ConstructorActivityLogRecord(Guid idUsuario, DateTime dataHora, LogAction acao, string dispositivosLogado, string localizacao)
        {
            IdLog = Guid.NewGuid();
            IdUsuario = idUsuario;
            DataHora = dataHora;
            Acao = acao;
            DispositivoLogado = dispositivosLogado;
            Localizacao = localizacao;
        }
    }
}
