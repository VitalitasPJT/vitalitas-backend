using Microsoft.EntityFrameworkCore;
using Domain.Features.Gym.Entities;
using Domain.Features.Records.TrainingSheet.Entities;
using Domain.Features.Records.MedicalRecord.Entities;
using Domain.Features.Plans.Contract.Entities;
using Domain.Features.Plans.License.Entities;
using Domain.Features.Shared.Entities;
using Domain.Features.Token.Entities;
using Domain.Features.Users.Administrator.Entities;
using Domain.Features.Users.Member.Entities;
using Domain.Features.Users.Common.Entities;
using Domain.Features.Users.Employee.Entities;
using Domain.Features.Users.Manager.Entities;
using Domain.Features.Users.Instructor.Entities;

namespace Infrastructure.Database.Context
{
    /// <summary>
    /// DbContext usado exclusivamente para versionar e aplicar o schema (Code-First/migrations)
    /// no Azure SQL centralizado. O acesso a dados em runtime continua via Dapper/repositories
    /// (ver Infrastructure/Repositories) — ver ADR-0010.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Usuarios => Set<User>();
        public DbSet<UserPhone> TelefonesUsuario => Set<UserPhone>();
        public DbSet<Member> Alunos => Set<Member>();
        public DbSet<Attendance> Frequencias => Set<Attendance>();
        public DbSet<XpHistory> XpHistoricos => Set<XpHistory>();
        public DbSet<Manager> Gestores => Set<Manager>();
        public DbSet<Instructor> Instrutores => Set<Instructor>();
        public DbSet<Employee> Funcionarios => Set<Employee>();
        public DbSet<Administrator> Administradores => Set<Administrator>();

        public DbSet<Gym> Academias => Set<Gym>();
        public DbSet<GymPhone> TelefonesAcademia => Set<GymPhone>();

        public DbSet<Contract> Contratos => Set<Contract>();
        public DbSet<ContractPlan> PlanosContrato => Set<ContractPlan>();
        public DbSet<License> Licencas => Set<License>();
        public DbSet<LicensePlan> PlanosLicenca => Set<LicensePlan>();

        public DbSet<TrainingSheet> Fichas => Set<TrainingSheet>();
        public DbSet<MedicalRecord> FichasMedicas => Set<MedicalRecord>();

        public DbSet<Schedule> Agendas => Set<Schedule>();
        //public DbSet<Avaliacao> Avaliacoes => Set<Avaliacao>();
        public DbSet<Workout> Treinos => Set<Workout>();
        public DbSet<ActivityLog> LogsAtividade => Set<ActivityLog>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
