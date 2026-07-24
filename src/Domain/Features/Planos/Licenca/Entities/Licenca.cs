using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Planos.Licenca.Entities
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

        public Licenca(Guid idPlano, Monetario mensalidade, StatusLicenca status, TipoLicenca tipo, DateOnly dataFim, DateOnly dateAssinatura, string caminhoPdf)
        {
            IdLicenca = Guid.NewGuid();
            IdPlano = idPlano;
            Mensalidade = mensalidade;
            Status = status;
            Tipo = tipo;
            DataFim = dataFim;
            DateAssinatura = dateAssinatura;
            CaminhoPdf = caminhoPdf;
        }

        public Licenca() {}

    }
}
