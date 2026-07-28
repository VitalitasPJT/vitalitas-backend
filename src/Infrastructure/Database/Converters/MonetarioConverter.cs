using System.Globalization;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class MonetarioConverter : ValueConverter<Monetario, decimal>
    {
        public MonetarioConverter()
            : base(
                monetario => decimal.Parse(monetario.Valor, CultureInfo.InvariantCulture),
                valor => new Monetario(valor.ToString(CultureInfo.InvariantCulture)))
        {
        }
    }
}
