using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class NomeConverter : ValueConverter<Nome, string>
    {
        public NomeConverter()
            : base(
                nome => nome.Valor,
                valor => Nome.Create(valor))
        {
        }
    }
}
