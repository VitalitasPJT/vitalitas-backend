using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IUsuario
    {
        Usuario Login(string email, string senha);
        void TrocarSenha(Guid idusuario, string novasenha);
        
    }
}