using Domain.Features.Plans.License.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Plans.License
{
    public class LicenseConfiguration : IEntityTypeConfiguration<Domain.Features.Plans.License.Entities.License>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Plans.License.Entities.License> builder)
        {
            builder.ToTable("License");

            builder.HasKey(l => l.IdLicenca);
            builder.Property(l => l.IdLicenca).ValueGeneratedNever();

            builder.Property(l => l.IdPlano).IsRequired();

            builder.Property(l => l.Mensalidade)
                .HasConversion(v => v.Valor, v => new Monetary(v))
                .HasColumnName("Mensalidade")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(l => l.Status).HasColumnName("Status").IsRequired();
            builder.Property(l => l.Tipo).HasColumnName("Tipo").IsRequired();
            builder.Property(l => l.DataFim).HasColumnName("DataFim").IsRequired();
            builder.Property(l => l.DateAssinatura).HasColumnName("DataAssinatura").IsRequired();
            builder.Property(l => l.CaminhoPdf).HasColumnName("CaminhoPdf").HasMaxLength(500);
        }
    }
}
