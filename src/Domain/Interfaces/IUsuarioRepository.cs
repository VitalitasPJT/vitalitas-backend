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
        LogAtividade RegistrarAcao(Guid idUsuario, LogAtividade acao);
        string GetTipoUsuario(Guid idUsuario);        
    }
}