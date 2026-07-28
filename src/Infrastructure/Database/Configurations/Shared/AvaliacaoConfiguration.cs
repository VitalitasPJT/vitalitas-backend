using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(e => e.IdAvaliacao);
            builder.Property(e => e.Sexo).HasMaxLength(10);
            builder.HasIndex(e => new { e.IdAcademia, e.IdAvaliacao });
        }
    }
}
