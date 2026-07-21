using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Features.Shared.Entities
{
    public class LogAtividade
    {
        public Guid IdLog { get; private set; }
        public Guid IdUsuario { get; private set; }
        public DateTime DataHora { get; private set; }
        public AcaoLog Acao { get; private set; }
        public string DispositivoLogado { get; private set; }
        public string Localizacao { get; private set; }

        public LogAtividade(Guid idUsuario, AcaoLog acao, string dispositivoLogado, string localizacao)
        {
            IdLog = Guid.NewGuid();
            IdUsuario = idUsuario;
            DataHora = DateTime.UtcNow;
            Acao = acao;
            DispositivoLogado = dispositivoLogado;
            Localizacao = localizacao;
        }

        public LogAtividade() {}
     }
}