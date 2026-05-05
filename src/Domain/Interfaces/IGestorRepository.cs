using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Vitalitas.Domain.Interfaces
{
    public interface IGestorRepository
    {
        dynamic CriarGestor(Guid idUsuario);
        dynamic CriarFuncionario(Guid idUsuario, Cargo cargo);
        dynamic CriarUsuario(Guid idAcademia, string nome, string email, string senha, DateOnly dataNascimento, string cpf, TipoUsuario tipoUsuario, string quadra, string rua, string bairro, string cidade, string estado, string cep);
        dynamic CriarAluno(Guid idInstrutor, Guid idUsuario, int idContrato, Guid idAcademia, string objetivo);
        Guid CriarInstrutor(Instrutor instrutor);
        dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo);
        dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario);
        dynamic ObterGestor(Guid idGestor);
        dynamic ObterAcademiaGestor(Guid idGestor);
        List<Usuario> ListarUsuarios(Guid idAcademia);
        Usuario ListarUsuario(Guid idusuario);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);

    }
}
