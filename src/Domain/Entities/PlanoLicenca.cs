using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PlanoLicenca
    {
        public Guid IdPlano {  get; private set; }
        public string Nome {  get; private set; }
        public string Descricao { get; private set; }
        public Monetario Valor {  get; private set; }
    }
}
