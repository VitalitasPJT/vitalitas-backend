using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Records
{
    public class UsuarioDB
    {
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Quadra { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; } // SQL Server usa DateTime, não DateOnly
        public string Cpf { get; set; }
        public int TipoUsuario { get; set; }
        public bool Flag { get; set; }
    }
}