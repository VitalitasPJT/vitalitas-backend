using Domain.Features.Planos.Licenca.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos.Licenca
{
    public class LicencaConfiguration : IEntityTypeConfiguration<Domain.Features.Planos.Licenca.Entities.Licenca>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Planos.Licenca.Entities.Licenca> builder)
        {
            builder.ToTable("Licenca");

            builder.HasKey(l => l.IdLicenca);
            builder.Property(l => l.IdLicenca).ValueGeneratedNever();

            builder.Property(l => l.IdPlano).IsRequired();

            builder.Property(l => l.Mensalidade)
                .HasConversion(v => v.Valor, v => new Monetario(v))
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
