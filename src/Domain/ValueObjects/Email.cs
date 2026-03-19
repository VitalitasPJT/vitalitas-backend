using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
   public record Email
    {
        public string Valor { get; init; }

        public Email() { }

        public Email(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("O email não pode ser vazio.");

            if (!valor.Contains("@") || !valor.Contains("."))
                throw new ArgumentException("O email fornecido é inválido.");

            Valor = valor;
        }

    }
}