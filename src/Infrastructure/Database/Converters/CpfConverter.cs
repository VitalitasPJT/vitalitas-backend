using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Database.Converters
{
    public class CpfConverter : ValueConverter<CPF, string>
    {
        public CpfConverter()
            : base(
                cpf => cpf.Valor,
                valor => new CPF(valor))
        {
        }
    }
}
