using Domain.Features.Usuarios.Gestor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Gestor
{
    public class GestorConfiguration : IEntityTypeConfiguration<Domain.Features.Usuarios.Gestor.Entities.Gestor>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Usuarios.Gestor.Entities.Gestor> builder)
        {
            builder.ToTable("Gestor");

            builder.HasKey(g => g.IdGestor);
            builder.Property(g => g.IdGestor).ValueGeneratedNever();

            builder.Property(g => g.IdUsuario).IsRequired();
        }
    }
}
