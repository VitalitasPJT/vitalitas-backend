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
        dynamic TrocarSenha(Guid idusuario, string novasenha);
        Guid CriarUsuario(string nome, string email, string senha, string quadra, string rua, string bairro, string cidade, string estado, string cep, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario);
        dynamic AtualizarDados(Guid idusuario, dynamic var, string atributo);
        dynamic Desativar(Guid idusuario);
        dynamic Ativar(Guid idusuario);
        dynamic AdicionarLog(Guid idusuario, LogAtividade log);
        List<dynamic> ObterLogs(Guid idusuario);
    }
}