using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Funcionario
    {
        public Guid IdFuncionario { get; private set; }
        public Guid IdUsuario { get; private set; }
        public CREF CREF { get; private set; }
    }
}