using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Plans.Contract.Entities
{
    public class ContractPlan
    {
        public Guid IdPlano { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Monetary Valor { get; private set; }

        public ContractPlan(string nome, string descricao, Monetary valor)
        {
            IdPlano = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }

        public ContractPlan() { }
    }
}
