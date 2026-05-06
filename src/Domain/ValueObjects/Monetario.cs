using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Monetario
    {
        private string valor;

        public Monetario(string valor)
        {
            this.valor = valor;
        }
    }
}

