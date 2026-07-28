using System.Dynamic;
using Domain.Enums;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Gestor.Interfaces;
using Domain.Features.Usuarios.Instrutor.Entities;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Usuarios.Gestor
{
    public class GestorRepository : IGestorRepository
    {
        private readonly VitalitasDbContext _context;

        public GestorRepository(VitalitasDbContext context)
        {
            _context = context;
        }

        public (bool, Guid) CriarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, usuario.IdUsuario) : (false, Guid.Empty);
        }

        public (bool, Guid) CriarAluno(Domain.Features.Usuarios.Aluno.Entities.Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, aluno.IdAluno) : (false, Guid.Empty);
        }

        public (bool, Guid) CriarInstrutor(Instrutor instrutor)
        {
            _context.Instrutores.Add(instrutor);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, instrutor.IdInstrutor) : (false, Guid.Empty);
        }

        public (bool, Guid) CriarFuncionario(Guid idUsuario, Cargo cargo)
        {
            var funcionario = new Domain.Features.Usuarios.Funcionario.Entities.Funcionario(Guid.NewGuid(), idUsuario, cargo);

            _context.Funcionarios.Add(funcionario);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, funcionario.IdFuncionario) : (false, Guid.Empty);
        }

        public (bool, Guid) CriarGestor(Guid idUsuario)
        {
            var gestor = new Domain.Features.Usuarios.Gestor.Entities.Gestor(idUsuario);

            _context.Gestores.Add(gestor);
            var rowsAffected = _context.SaveChanges();

            return rowsAffected > 0 ? (true, gestor.IdGestor) : (false, Guid.Empty);
        }

        public List<dynamic> ListarAlunos(Guid idAcademia)
        {
            var joined = (from a in _context.Alunos
                          join u in _context.Usuarios on a.IdUsuario equals u.IdUsuario
                          where u.IdAcademia == idAcademia
                          select new { Aluno = a, Usuario = u })
                          .AsNoTracking()
                          .ToList();

            return joined.Select(x =>
            {
                dynamic item = new ExpandoObject();
                item.idAluno = x.Aluno.IdAluno;
                item.idAcademia = x.Usuario.IdAcademia;
                item.idUsuario = x.Aluno.IdUsuario;
                item.tipoUsuario = (int)x.Usuario.TipoUsuario;
                item.objetivo = x.Aluno.Objetivo;
                item.nome = x.Usuario.Nome.Valor;
                item.email = x.Usuario.Email.Valor;
                return (dynamic)item;
            }).ToList();
        }

        public List<dynamic> ListarUsuarios(Guid idAcademia)
        {
            var usuarios = _context.Usuarios
                .AsNoTracking()
                .Where(u => u.IdAcademia == idAcademia)
                .ToList();

            return usuarios.Select(u =>
            {
                dynamic item = new ExpandoObject();
                item.idUsuario = u.IdUsuario;
                item.idAcademia = u.IdAcademia;
                item.nome = u.Nome.Valor;
                item.email = u.Email.Valor;
                item.tipoUsuario = (int)u.TipoUsuario;
                item.ativo = u.Ativo;
                return (dynamic)item;
            }).ToList();
        }

        public dynamic ObterGestor(Guid idGestor)
        {
            var joined = (from u in _context.Usuarios
                          join g in _context.Gestores on u.IdUsuario equals g.IdUsuario
                          where g.IdGestor == idGestor
                          select new { Usuario = u, Gestor = g })
                          .AsNoTracking()
                          .FirstOrDefault();

            if (joined == null)
                throw new Exception("Gestor não encontrado");

            dynamic result = new ExpandoObject();
            result.idUsuario = joined.Usuario.IdUsuario;
            result.idGestor = joined.Gestor.IdGestor;
            result.idAcademia = joined.Usuario.IdAcademia;
            result.nome = joined.Usuario.Nome.Valor;
            result.email = joined.Usuario.Email.Valor;
            result.CPF = joined.Usuario.CPF.Valor;

            return result;
        }

        public (dynamic, int) ListarUsuario(Guid idusuario)
        {
            var usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(u => u.IdUsuario == idusuario);
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            dynamic result = new ExpandoObject();
            result.idUsuario = usuario.IdUsuario;
            result.idAcademia = usuario.IdAcademia;
            result.nome = usuario.Nome.Valor;
            result.email = usuario.Email.Valor;
            result.dataNascimento = usuario.DataNascimento;
            result.CPF = usuario.CPF.Valor;
            result.tipoUsuario = (int)usuario.TipoUsuario;
            result.ativo = usuario.Ativo;
            result.quadra = usuario.Quadra;
            result.rua = usuario.Rua;
            result.bairro = usuario.Bairro;
            result.cidade = usuario.Cidade;
            result.estado = usuario.Estado;
            result.cep = usuario.CEP;

            // Números de case alinhados com o enum real Domain.Enums.TipoUsuario
            // (Instrutor=1, Aluno=2, Gestor=3, Administrador=4) — antes dessa correção,
            // os números não batiam com o enum e o campo específico do tipo nunca era
            // preenchido para nenhum usuário real, quebrando GestorUC.ListarUsuario.
            int tipoUsuario = (int)usuario.TipoUsuario;

            // Propriedades específicas por tipo sempre existem no ExpandoObject, mesmo
            // quando o registro filho não é encontrado — evita RuntimeBinderException em
            // GestorUC.ListarUsuario, que lê essas propriedades sem checar existência.
            switch (tipoUsuario)
            {
                case (int)TipoUsuario.Instrutor:
                    result.idInstrutor = Guid.Empty;
                    result.cref = string.Empty;
                    var instrutor = _context.Instrutores.AsNoTracking().FirstOrDefault(i => i.IdUsuario == idusuario);
                    if (instrutor != null)
                    {
                        result.idInstrutor = instrutor.IdInstrutor;
                        result.cref = instrutor.CREF.Valor;
                    }
                    break;
                case (int)TipoUsuario.Aluno:
                    result.idAluno = Guid.Empty;
                    result.idInstrutor = Guid.Empty;
                    result.idContrato = Guid.Empty;
                    result.objetivo = string.Empty;
                    var aluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.IdUsuario == idusuario);
                    if (aluno != null)
                    {
                        result.idAluno = aluno.IdAluno;
                        result.idInstrutor = aluno.IdInstrutor;
                        result.idContrato = aluno.IdContrato;
                        result.objetivo = aluno.Objetivo;
                    }
                    break;
                case (int)TipoUsuario.Gestor:
                    var gestor = _context.Gestores.AsNoTracking().FirstOrDefault(g => g.IdUsuario == idusuario);
                    if (gestor != null)
                    {
                        result.idGestor = gestor.IdGestor;
                    }
                    break;
                case (int)TipoUsuario.Administrador:
                    result.idFuncionario = Guid.Empty;
                    result.cargo = string.Empty;
                    var administrador = _context.Administradores.AsNoTracking().FirstOrDefault(ad => ad.IdUsuario == idusuario);
                    if (administrador != null)
                    {
                        result.idFuncionario = administrador.IdFuncionario;
                        result.cargo = administrador.Cargo.ToString();
                    }
                    break;
                default:
                    throw new Exception("Tipo de usuário inválido");
            }

            return (result, tipoUsuario);
        }

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

        public dynamic Desativar(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public dynamic ObterAcademiaGestor(Guid idGestor)
        {
            throw new NotImplementedException();
        }
    }
}
