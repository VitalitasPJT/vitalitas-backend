using Domain.Enums;
using Domain.Features.Shared.Entities;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Common.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Usuarios.Common
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly VitalitasDbContext _context;

        public UsuarioRepository(VitalitasDbContext context)
        {
            _context = context;
        }

        public Usuario Login(string email, string senha)
        {
            var candidateEmail = new Email(email);
            return _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == candidateEmail && u.Senha == senha);
        }

        public LogAtividade RegistrarAcao(Guid idusuario, int acao, string dispositivoLogado, string localizacao)
        {
            var logAtividade = new LogAtividade(idusuario, (AcaoLog)acao, dispositivoLogado, localizacao);

            _context.LogsAtividade.Add(logAtividade);
            _context.SaveChanges();

            return logAtividade;
        }

        public TipoUsuario? GetTipoUsuario(Guid idUsuario)
        {
            var usuario = _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.IdUsuario == idUsuario);

            return usuario?.TipoUsuario;
        }

        public Guid GetIdAcademia(Guid idUsuario)
        {
            var usuario = _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.IdUsuario == idUsuario);

            if (usuario == null)
                throw new Exception($"Academia não encontrada para o usuário {idUsuario}");

            return usuario.IdAcademia;
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
    }
}
