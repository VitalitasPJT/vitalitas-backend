using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ValueObjects;
using static Application.DTOs.UsuarioRP;

namespace Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        LoginResponse Login(Email email, string senha);
        TrocarSenhaResponse TrocarSenha(Guid idusuario, string novasenha);
    }
}