using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class CrefConverter : ValueConverter<CREF, string>
    {
        public CrefConverter()
            : base(
                cref => cref.Valor,
                valor => new CREF(valor))
        {
        }
    }
}
