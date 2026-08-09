using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Plans.Contract.Entities
{
    public class Contract
    {
        public Guid IdContrato { get; private set; }
        public Guid IdPlanoContrato { get; private set; }
        public Monetary Mensalidade { get; private set; }
        public ContractStatus Status { get; private set; }
        public DateOnly DataFim { get; private set; }
        public DateOnly DataAssinatura { get; private set; }
        
    }
}