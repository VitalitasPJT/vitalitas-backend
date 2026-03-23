using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class CPF
    {
        private string cpf;

        public CPF(string cpf)
        {
            this.cpf = cpf;
        }
    }
}