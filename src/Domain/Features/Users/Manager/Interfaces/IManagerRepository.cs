using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Features.Users.Common.Entities;
using Domain.Enums;

namespace Domain.Features.Users.Manager.Interfaces
{
    public interface IManagerRepository
    {
        //Implementado
        (bool, Guid) CriarUsuario(User usuario);
        (bool, Guid) CriarAluno(Domain.Features.Users.Member.Entities.Member aluno);
        (bool, Guid) CriarInstrutor(Domain.Features.Users.Instructor.Entities.Instructor instrutor);
        (bool, Guid) CriarFuncionario(Guid idUsuario, Role cargo);
        (bool, Guid) CriarGestor(Guid idUsuario);
        List<dynamic> ListarAlunos(Guid idAcademia);
        List<dynamic> ListarUsuarios(Guid idAcademia);
        dynamic ObterGestor(Guid idUsuario);
        (dynamic, int) ListarUsuario(Guid idusuario);

        //Não implementado
        dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo);
        dynamic AtualizarFilho(Guid id, dynamic var, string atributo, UserType tipoUsuario);
        dynamic ObterAcademiaGestor(Guid idGestor);
        dynamic Desativar(Guid idUsuario);
        dynamic Ativar(Guid idUsuario);
    }
}
