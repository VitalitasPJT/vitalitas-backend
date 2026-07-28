using Domain.Features.Planos.Contrato.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos
{
    public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(e => e.IdContrato);

            builder.Property(e => e.Mensalidade)
                .HasConversion<MonetarioConverter>()
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
