using Domain.Features.Planos.Licenca.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos
{
    public class LicencaConfiguration : IEntityTypeConfiguration<Licenca>
    {
        public void Configure(EntityTypeBuilder<Licenca> builder)
        {
            builder.HasKey(e => e.IdLicenca);

            builder.Property(e => e.Mensalidade)
                .HasConversion<MonetarioConverter>()
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(e => e.CaminhoPdf).HasMaxLength(255);
        }
    }
}
