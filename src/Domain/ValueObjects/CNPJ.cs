using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class CNPJ
    {
        public string Valor { get; private set; }

        public CNPJ(string cnpj)
        {
            Valor = cnpj;
        }

        public override string ToString() => Valor;
    }
}
