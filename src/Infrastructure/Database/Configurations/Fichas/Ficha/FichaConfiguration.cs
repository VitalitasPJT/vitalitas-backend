using Domain.Features.Fichas.Ficha.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Fichas.Ficha
{
    public class FichaConfiguration : IEntityTypeConfiguration<Domain.Features.Fichas.Ficha.Entities.Ficha>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Fichas.Ficha.Entities.Ficha> builder)
        {
            builder.ToTable("Ficha");

            builder.HasKey(f => f.IdFicha);
            builder.Property(f => f.IdFicha).ValueGeneratedNever();

            builder.Property(f => f.IdAvaliacao).IsRequired();
            builder.Property(f => f.NomeFicha).HasColumnName("NomeFicha").HasMaxLength(200);
            builder.Property(f => f.Observacoes).HasColumnName("Observacoes").HasMaxLength(1000);
        }
    }
}
