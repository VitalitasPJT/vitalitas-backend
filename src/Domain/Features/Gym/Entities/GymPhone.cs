using System;

namespace Domain.Features.Gym.Entities
{
    public class GymPhone
    {
        public Guid IdTelefone { get; private set; }
        public Guid IdAcademia { get; private set; }
        public string Telefone { get; private set; }
    }
}
