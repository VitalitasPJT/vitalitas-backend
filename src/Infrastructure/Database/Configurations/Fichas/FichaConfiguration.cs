using Domain.Features.Fichas.Ficha.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Fichas
{
    public class FichaConfiguration : IEntityTypeConfiguration<Ficha>
    {
        public void Configure(EntityTypeBuilder<Ficha> builder)
        {
            builder.HasKey(e => e.IdFicha);
            builder.Property(e => e.NomeFicha).HasMaxLength(100);
            builder.Property(e => e.Observacoes).HasMaxLength(255);
            builder.HasIndex(e => new { e.IdAcademia, e.IdFicha });
        }
    }
}
