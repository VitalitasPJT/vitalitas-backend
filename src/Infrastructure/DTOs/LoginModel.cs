using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Infrastructure.DTOs
{
    public class LoginModel
    {
        public TipoUsuario TipoUsuario { get; set; }
        public Guid IdUsuario { get; set;}
        public bool Flag {get; set;}
    }
}