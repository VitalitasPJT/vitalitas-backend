using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IGestorRepository
    {
        dynamic CriarGestor(Guid idUsuario);
        dynamic CriarFuncionario(Guid idUsuario, Cargo cargo);
        (bool, Guid) CriarUsuario(Usuario usuario);
        (bool, Guid) CriarAluno(Aluno aluno);
        (bool, Guid) CriarInstrutor(Instrutor instrutor);
        dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo);
        dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario);
        dynamic ObterGestor(Guid idGestor);
        dynamic ObterAcademiaGestor(Guid idGestor);
        List<Usuario> ListarUsuarios(Guid idAcademia);
        List<dynamic> ListarAlunos(Guid idAcademia);
        Usuario ListarUsuario(Guid idusuario);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);
    }
}
