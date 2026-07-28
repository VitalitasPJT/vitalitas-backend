namespace Domain.Features.Fichas.Ficha.Entities
{
    public class Ficha
    {
        public Guid IdFicha { get; private set; }
        public Guid IdAcademia { get; private set; }
        public Guid IdAvaliacao { get; private set; }
        public string NomeFicha { get; private set; }
        public string Observacoes { get; private set; }
    }
}
