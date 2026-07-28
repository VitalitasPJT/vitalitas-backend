using Domain.Features.Planos.Licenca.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos
{
    public class PlanoLicencaConfiguration : IEntityTypeConfiguration<PlanoLicenca>
    {
        public void Configure(EntityTypeBuilder<PlanoLicenca> builder)
        {
            builder.HasKey(e => e.IdPlanoLicenca);
            builder.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Descricao).HasMaxLength(255);

            builder.Property(e => e.Valor)
                .HasConversion<MonetarioConverter>()
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
