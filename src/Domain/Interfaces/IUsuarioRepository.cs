using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Interfaces
{
    public interface IUsuario
    {
        Usuario Login(string email, string senha);
        LogAtividade RegistrarAcao(Guid idUsuario, int acao, string dispositivoLogado, string localizacao);
        TipoUsuario? GetTipoUsuario(Guid idUsuario);
        bool TrocarSenha(Guid idusuario, string novasenha);
    }
}