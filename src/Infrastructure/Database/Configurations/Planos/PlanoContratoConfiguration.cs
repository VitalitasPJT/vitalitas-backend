using Domain.Features.Planos.Contrato.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos
{
    public class PlanoContratoConfiguration : IEntityTypeConfiguration<PlanoContrato>
    {
        public void Configure(EntityTypeBuilder<PlanoContrato> builder)
        {
            builder.HasKey(e => e.IdPlano);
            builder.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Descricao).HasMaxLength(255);

            builder.Property(e => e.Valor)
                .HasConversion<MonetarioConverter>()
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
