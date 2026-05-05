using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PlanoLicenca
    {
        public Guid IdPlanoLicenca {  get; private set; }
        public string Nome {  get; private set; }
        public string Descricao { get; private set; }
        public Monetario Valor {  get; private set; }

        public PlanoLicenca(string nome, string descricao, Monetario valor)
        {
            IdPlanoLicenca = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public PlanoLicenca() {}
    }
}
