using Domain.Features.Plans.License.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Plans.License
{
    public class LicensePlanConfiguration : IEntityTypeConfiguration<LicensePlan>
    {
        public void Configure(EntityTypeBuilder<LicensePlan> builder)
        {
            builder.ToTable("LicensePlan");

            builder.HasKey(p => p.IdPlanoLicenca);
            builder.Property(p => p.IdPlanoLicenca).ValueGeneratedNever();

            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Descricao).HasColumnName("Descricao").HasMaxLength(1000);

            builder.Property(p => p.Valor)
                .HasConversion(v => v.Valor, v => new Monetary(v))
                .HasColumnName("Valor")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
