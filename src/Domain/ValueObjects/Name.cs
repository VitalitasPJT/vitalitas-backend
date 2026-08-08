using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public record Name
    {
        public string Valor { get; init; }

        public Name(string valor)
        {
            Valor = valor;
        }

        public static Name Create(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O nome não pode ser vazio.");

            return new Name(valor);
        }
    }
}