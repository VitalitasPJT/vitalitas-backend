using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Plans.License.Entities
{
    public class License
    {
        public Guid IdLicenca { get; private set; }
        public Guid IdPlano { get; private set; }
        public Monetary Mensalidade { get; private set; }
        public LicenseStatus Status { get; private set; }
        public LicenseType Tipo { get; private set; }
        public DateOnly DataFim { get; private set; }
        public DateOnly DateAssinatura { get; private set; }
        public string CaminhoPdf { get; private set; }

        public License(Guid idPlano, Monetary mensalidade, LicenseStatus status, LicenseType tipo, DateOnly dataFim, DateOnly dateAssinatura, string caminhoPdf)
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

        public License() {}

    }
}
