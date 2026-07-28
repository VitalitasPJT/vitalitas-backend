using Domain.Features.Usuarios.Gestor.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class GestorConfiguration : IEntityTypeConfiguration<Gestor>
    {
        public void Configure(EntityTypeBuilder<Gestor> builder)
        {
            builder.HasKey(e => e.IdGestor);
            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdGestor });
        }
    }
}
