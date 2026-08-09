using Domain.Features.Users.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Administrator
{
    public class AdministratorConfiguration : IEntityTypeConfiguration<Domain.Features.Users.Administrator.Entities.Administrator>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Users.Administrator.Entities.Administrator> builder)
        {
            builder.ToTable("Administrator");

            builder.HasKey(a => a.IdFuncionario);
            builder.Property(a => a.IdFuncionario).ValueGeneratedNever();

            builder.Property(a => a.IdUsuario).IsRequired();
            builder.Property(a => a.Cargo).HasColumnName("Cargo").IsRequired();
        }
    }
}
