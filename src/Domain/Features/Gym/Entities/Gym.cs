using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Features.Gym.Entities
{
    public class Gym
    {
        public Guid IdAcademia { get; private set; }
        public Guid IdLicenca {  get; private set; }
        public Guid IdGestor { get; private set; }
        public Name NomeAcademia { get; private set; }
        public CNPJ CNPJ { get; private set; }
        public string Quadra { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public GymType TipoAcademia { get; private set; }
        public Email EmailInstitucional { get; private set; }

        public Gym(Guid idlicenca, Guid idgestor, Name nomeacademia, CNPJ cnpj, string quadra, string rua, string bairro, string cidade, string estado, string cep, GymType tipoacademia, Email emailinstitucional)
        {
            IdAcademia = Guid.NewGuid();
            IdLicenca = idlicenca;
            IdGestor = idgestor;
            NomeAcademia = nomeacademia;
            CNPJ = cnpj;
            Quadra = quadra;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            TipoAcademia = tipoacademia;
            EmailInstitucional = emailinstitucional;
        }

        public Gym() { }
    }
}