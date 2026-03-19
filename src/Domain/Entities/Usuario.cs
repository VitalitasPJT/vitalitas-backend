using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid IdUsuario { get; private set; }
        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
        public string Endereço { get; private set; }
        /*public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Quadra { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }*/
        public string Senha { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public CPF CPF { get; private set; }
        public TipoUsuario TipoUsuario { get; private set; }
        public bool Flag { get; private set; }
        public List<Guid> IdLog { get; private set; }
    }
}