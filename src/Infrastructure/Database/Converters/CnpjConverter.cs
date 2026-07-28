using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class CnpjConverter : ValueConverter<CNPJ, string>
    {
        public CnpjConverter()
            : base(
                cnpj => cnpj.Valor,
                valor => new CNPJ(valor))
        {
        }
    }
}
