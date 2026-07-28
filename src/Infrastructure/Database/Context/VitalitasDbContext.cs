using Domain.Features.Academia.Entities;
using Domain.Features.Fichas.Ficha.Entities;
using Domain.Features.Fichas.FichaMedica.Entities;
using Domain.Features.Planos.Contrato.Entities;
using Domain.Features.Planos.Licenca.Entities;
using Domain.Features.Shared.Entities;
using Domain.Features.Token.Entities;
using Domain.Features.Usuarios.Administrador.Entities;
using Domain.Features.Usuarios.Aluno.Entities;
using Domain.Features.Usuarios.Common.Entities;
using Domain.Features.Usuarios.Funcionario.Entities;
using Domain.Features.Usuarios.Gestor.Entities;
using Domain.Features.Usuarios.Instrutor.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    public class VitalitasDbContext : DbContext
    {
        public VitalitasDbContext(DbContextOptions<VitalitasDbContext> options) : base(options)
        {
        }

        public DbSet<Academia> Academias => Set<Academia>();
        public DbSet<TelefoneAcademia> TelefonesAcademia => Set<TelefoneAcademia>();

        public DbSet<Ficha> Fichas => Set<Ficha>();
        public DbSet<FichaMedica> FichasMedicas => Set<FichaMedica>();

        public DbSet<Contrato> Contratos => Set<Contrato>();
        public DbSet<PlanoContrato> PlanosContrato => Set<PlanoContrato>();
        public DbSet<Licenca> Licencas => Set<Licenca>();
        public DbSet<PlanoLicenca> PlanosLicenca => Set<PlanoLicenca>();

        public DbSet<Agenda> Agendas => Set<Agenda>();
        public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();
        public DbSet<LogAtividade> LogsAtividade => Set<LogAtividade>();
        public DbSet<Treino> Treinos => Set<Treino>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Administrador> Administradores => Set<Administrador>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<TelefoneUsuario> TelefonesUsuario => Set<TelefoneUsuario>();
        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Frequencia> Frequencias => Set<Frequencia>();
        public DbSet<XpHistorico> XpHistoricos => Set<XpHistorico>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Gestor> Gestores => Set<Gestor>();
        public DbSet<Instrutor> Instrutores => Set<Instrutor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VitalitasDbContext).Assembly);
        }
    }
}
