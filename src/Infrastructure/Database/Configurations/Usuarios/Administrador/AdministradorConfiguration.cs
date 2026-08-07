using Domain.Features.Usuarios.Administrador.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Administrador
{
    public class AdministradorConfiguration : IEntityTypeConfiguration<Domain.Features.Usuarios.Administrador.Entities.Administrador>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Usuarios.Administrador.Entities.Administrador> builder)
        {
            builder.ToTable("Administrador");

            builder.HasKey(a => a.IdFuncionario);
            builder.Property(a => a.IdFuncionario).ValueGeneratedNever();

            builder.Property(a => a.IdUsuario).IsRequired();
            builder.Property(a => a.Cargo).HasColumnName("Cargo").IsRequired();
        }
    }
}
