using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities
{
    public class Academia
    {
        public Guid IdAcadenia { get; private set; }
        public Guid IdLicenca {  get; private set; }
        public Guid IdGestor { get; private set; }
        public Nome NomeAcademia { get; private set; }
        public CNPJ CNPJ { get; private set; }
        public string Quadra { get; private set; }
        public string Rua { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public TipoAcademia TipoAcademia { get; private set; }
        public Email EmailInstitucional { get; private set; }
    }
}