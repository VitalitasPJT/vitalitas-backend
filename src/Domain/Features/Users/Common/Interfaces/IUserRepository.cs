using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Features.Shared.Entities;
using Domain.Features.Users.Common.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Users.Common.Interfaces
{
    public interface IUserRepository
    {
        User Login(string email, string senha);
        ActivityLog RegistrarAcao(Guid idUsuario, int acao, string dispositivoLogado, string localizacao);
        UserType? GetTipoUsuario(Guid idUsuario);
        bool TrocarSenha(Guid idusuario, string novasenha);
        Guid GetIdAcademia(Guid idUsuario);
    }
}