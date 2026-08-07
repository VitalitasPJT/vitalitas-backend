using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Monetario
    {
        public string Valor { get; private set; }

        public Monetario(string valor)
        {
            Valor = valor;
        }

        public override string ToString() => Valor;
    }
}

