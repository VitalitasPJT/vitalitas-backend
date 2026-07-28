using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Planos.Contrato.Entities
{
    public class Contrato
    {
        public Guid IdContrato { get; private set; }
        public Guid IdPlanoContrato { get; private set; }
        public Monetario Mensalidade { get; private set; }
        public StatusContrato Status { get; private set; }
        public DateOnly DataFim { get; private set; }
        public DateOnly DataAssinatura { get; private set; }
        
    }
}