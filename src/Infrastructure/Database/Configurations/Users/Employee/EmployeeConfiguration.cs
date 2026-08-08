using Domain.Features.Users.Employee.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Employee
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Domain.Features.Users.Employee.Entities.Employee>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Users.Employee.Entities.Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(f => f.IdFuncionario);
            builder.Property(f => f.IdFuncionario).ValueGeneratedNever();

            builder.Property(f => f.IdUsuario).IsRequired();

            // Nota: FuncionarioRepository.CriarFuncionario (Dapper) grava Cargo como texto
            // (cargo.ToString()), enquanto o restante do sistema trata enums como int
            // (ex.: User.TipoUsuario). Mantido como int aqui por consistência com o
            // resto do schema; esse descompasso no repository é pré-existente e fica
            // como item separado a alinhar.
            builder.Property(f => f.Cargo).HasColumnName("Cargo").IsRequired();
        }
    }
}
