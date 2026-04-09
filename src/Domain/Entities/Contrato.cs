using Domain.Enums;
using Domain.ValueObjects;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Contrato
    {
        public Guid IdContrato { get; private set; }
        public Guid IdPlano { get; private set; }
        public Monetario Mensalidade { get; private set; }
        public StatusContrato Status { get; private set; }
        public DateOnly DataFim { get; private set; }
        public DateOnly DataAssinatura { get; private set; }
        
    }
}