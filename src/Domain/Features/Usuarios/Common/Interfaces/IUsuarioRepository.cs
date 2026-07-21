using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Features.Shared.Entities;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Features.Usuarios.Common.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(string email, string senha);
        LogAtividade RegistrarAcao(Guid idUsuario, int acao, string dispositivoLogado, string localizacao);
        TipoUsuario? GetTipoUsuario(Guid idUsuario);
        bool TrocarSenha(Guid idusuario, string novasenha);
        Guid GetIdAcademia(Guid idUsuario);
    }
}