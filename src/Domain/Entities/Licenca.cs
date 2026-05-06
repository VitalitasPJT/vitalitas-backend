using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Licenca
    {
        public Guid IdLicenca { get; private set; }
        public Guid IdPlano { get; private set; }
        public Monetario Mensalidade { get; private set; }
        public StatusLicenca Status { get; private set; }
        public TipoLicenca Tipo { get; private set; }
        public DateOnly DataFim { get; private set; }
        public DateOnly DateAssinatura { get; private set; }
        public string CaminhoPdf { get; private set; }

    }
}
