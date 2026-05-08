using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class CPF
    {
        public string Valor { get; private set; }

        public CPF(string cpf)
        {
            this.Valor = cpf;
        }

        public override string ToString() => Valor;
    }
}