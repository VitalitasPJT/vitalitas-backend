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
        //Implementado
        (bool, Guid) CriarUsuario(Usuario usuario);
        (bool, Guid) CriarAluno(Aluno aluno);
        (bool, Guid) CriarInstrutor(Instrutor instrutor);
        (bool, Guid) CriarFuncionario(Guid idUsuario, Cargo cargo);
        List<dynamic> ListarAlunos(Guid idAcademia);
        List<Usuario> ListarUsuarios(Guid idAcademia);

        //Não implementado
        dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo);
        dynamic CriarGestor(Guid idUsuario);
        dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario);
        dynamic ObterGestor(Guid idGestor);
        dynamic ObterAcademiaGestor(Guid idGestor);
        Usuario ListarUsuario(Guid idusuario);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);
    }
}
