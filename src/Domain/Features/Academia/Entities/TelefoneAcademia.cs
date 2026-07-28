using System;

namespace Domain.Features.Academia.Entities
{
    public class TelefoneAcademia
    {
        public Guid IdTelefone { get; private set; }
        public Guid IdAcademia { get; private set; }
        public string Telefone { get; private set; }
    }
}
