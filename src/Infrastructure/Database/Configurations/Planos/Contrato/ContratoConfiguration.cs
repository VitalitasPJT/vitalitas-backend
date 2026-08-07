using Domain.Features.Planos.Contrato.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos.Contrato
{
    public class ContratoConfiguration : IEntityTypeConfiguration<Domain.Features.Planos.Contrato.Entities.Contrato>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Planos.Contrato.Entities.Contrato> builder)
        {
            builder.ToTable("Contrato");

            builder.HasKey(c => c.IdContrato);
            builder.Property(c => c.IdContrato).ValueGeneratedNever();

            builder.Property(c => c.IdPlanoContrato).IsRequired();

            builder.Property(c => c.Mensalidade)
                .HasConversion(v => v.Valor, v => new Monetario(v))
                .HasColumnName("Mensalidade")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Status).HasColumnName("Status").IsRequired();
            builder.Property(c => c.DataFim).HasColumnName("DataFim").IsRequired();
            builder.Property(c => c.DataAssinatura).HasColumnName("DataAssinatura").IsRequired();
        }
    }
}
