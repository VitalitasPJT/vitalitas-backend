using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.Contexts
{
    public class GestorRepository : IGestorRepository
    {
        public dynamic Ativar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic AtualizarFilho(Guid id, dynamic var, string atributo, TipoUsuario tipoUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic AtualizarUsuario(Guid idusuario, dynamic var, string atributo)
        {
            throw new NotImplementedException();
        }

        public dynamic CriarAluno(Guid idInstrutor, Guid idUsuario, int idContrato, Guid idAcademia, string objetivo)
        {
            throw new NotImplementedException();
        }

        public dynamic CriarFuncionario(Guid idUsuario, Cargo cargo)
        {
            throw new NotImplementedException();
        }

        public dynamic CriarGestor(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public Guid CriarInstrutor(Instrutor instrutor)
        {
            throw new NotImplementedException();
        }

        public dynamic CriarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public dynamic Desativar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public Usuario ListarUsuario(Guid idusuario)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> ListarUsuarios(Guid idAcademia)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterAcademiaGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }
    }
}