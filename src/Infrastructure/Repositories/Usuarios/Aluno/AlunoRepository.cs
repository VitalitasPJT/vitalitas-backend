using System.Dynamic;
using Domain.Features.Usuarios.Aluno.Interfaces;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Usuarios.Aluno
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly VitalitasDbContext _context;

        public AlunoRepository(VitalitasDbContext context)
        {
            _context = context;
        }

        public bool AtualizarObjetivo(Guid idaluno, string novoobjetivo)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.IdAluno == idaluno);
            if (aluno == null)
                throw new Exception("Aluno não encontrado ou objetivo já é o mesmo.");

            _context.Entry(aluno).Property(a => a.Objetivo).CurrentValue = novoobjetivo;

            return _context.SaveChanges() > 0;
        }

        public dynamic ListarAluno(Guid aluno)
        {
            var joined = (from a in _context.Alunos
                          join u in _context.Usuarios on a.IdUsuario equals u.IdUsuario
                          where a.IdAluno == aluno
                          select new { Aluno = a, Usuario = u })
                          .AsNoTracking()
                          .FirstOrDefault();

            if (joined == null)
                throw new Exception("Aluno não encontrado.");

            dynamic result = new ExpandoObject();
            result.idAluno = joined.Aluno.IdAluno;
            result.idAcademia = joined.Usuario.IdAcademia;
            result.idUsuario = joined.Aluno.IdUsuario;
            result.objetivo = joined.Aluno.Objetivo;
            result.nome = joined.Usuario.Nome.Valor;
            result.email = joined.Usuario.Email.Valor;
            result.tipoUsuario = (int)joined.Usuario.TipoUsuario;

            return result;
        }

        public bool TrocarSenha(Guid idusuario, string novasenha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == idusuario);
            if (usuario == null)
                return false;

            if (usuario.Senha == novasenha)
                throw new Exception("A nova senha não pode ser igual à senha atual.");

            var entry = _context.Entry(usuario);
            entry.Property(u => u.Senha).CurrentValue = novasenha;
            entry.Property(u => u.Flag).CurrentValue = false;

            return _context.SaveChanges() > 0;
        }

        public bool VincularInstrutor(Guid idaluno, Guid idprofessor)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.IdAluno == idaluno);
            if (aluno == null)
                throw new Exception("Aluno ou instrutor não encontrado, ou já estão vinculados.");

            _context.Entry(aluno).Property(a => a.IdInstrutor).CurrentValue = idprofessor;

            return _context.SaveChanges() > 0;
        }

        public Guid? ObterIdUsuarioPorAluno(Guid idAluno)
        {
            return _context.Alunos
                .AsNoTracking()
                .Where(a => a.IdAluno == idAluno)
                .Select(a => (Guid?)a.IdUsuario)
                .FirstOrDefault();
        }
    }
}
