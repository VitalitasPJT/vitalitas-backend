using Domain.Features.Records.TrainingSheet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Records.TrainingSheet
{
    public class TrainingSheetConfiguration : IEntityTypeConfiguration<Domain.Features.Records.TrainingSheet.Entities.TrainingSheet>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Records.TrainingSheet.Entities.TrainingSheet> builder)
        {
            builder.ToTable("TrainingSheet");

            builder.HasKey(f => f.IdFicha);
            builder.Property(f => f.IdFicha).ValueGeneratedNever();

            builder.Property(f => f.IdAvaliacao).IsRequired();
            builder.Property(f => f.NomeFicha).HasColumnName("NomeFicha").HasMaxLength(200);
            builder.Property(f => f.Observacoes).HasColumnName("Observacoes").HasMaxLength(1000);
        }
    }
}
