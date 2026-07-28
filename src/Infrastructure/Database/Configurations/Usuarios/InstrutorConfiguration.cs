using Domain.Features.Usuarios.Instrutor.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
    {
        public void Configure(EntityTypeBuilder<Instrutor> builder)
        {
            builder.HasKey(e => e.IdInstrutor);

            builder.Property(e => e.CREF)
                .HasConversion<CrefConverter>()
                .HasMaxLength(30);

            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdInstrutor });
        }
    }
}
