using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class CREF
    {
        public string Valor { get; private set; }

        public CREF(string cref)
        {
            this.Valor = cref;
        }

        public override string ToString() => Valor;
    }
}