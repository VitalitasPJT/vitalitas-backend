using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Nome
    {
        public string Valor { get; init; }

        public Nome(string valor)
        {
            Valor = valor;
        }

        public static Nome Create(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O nome não pode ser vazio.");

            return new Nome(valor);
        }
    }
}