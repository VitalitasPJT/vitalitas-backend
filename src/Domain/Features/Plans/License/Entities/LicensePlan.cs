using Domain.ValueObjects;

namespace Domain.Features.Plans.License.Entities
{
    public class LicensePlan
    {
        public Guid IdPlanoLicenca {  get; private set; }
        public string Nome {  get; private set; }
        public string Descricao { get; private set; }
        public Monetary Valor {  get; private set; }

        public LicensePlan(string nome, string descricao, Monetary valor)
        {
            IdPlanoLicenca = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public LicensePlan() {}
    }
}
