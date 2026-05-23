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
        (bool, Guid) CriarGestor(Guid idUsuario);
        List<dynamic> ListarAlunos(Guid idAcademia);
        List<dynamic> ListarUsuarios(Guid idAcademia);
        dynamic ObterGestor(Guid idUsuario);
        (dynamic, int) ListarUsuario(Guid idusuario);

        //Não implementado
        dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo);
        dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario);
        dynamic ObterAcademiaGestor(Guid idGestor);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);
    }
}
