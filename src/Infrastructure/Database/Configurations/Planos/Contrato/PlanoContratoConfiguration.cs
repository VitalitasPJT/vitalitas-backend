using Domain.Features.Planos.Contrato.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos.Contrato
{
    public class PlanoContratoConfiguration : IEntityTypeConfiguration<PlanoContrato>
    {
        public void Configure(EntityTypeBuilder<PlanoContrato> builder)
        {
            builder.ToTable("PlanoContrato");

            builder.HasKey(p => p.IdPlano);
            builder.Property(p => p.IdPlano).ValueGeneratedNever();

            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Descricao).HasColumnName("Descricao").HasMaxLength(1000);

            builder.Property(p => p.Valor)
                .HasConversion(v => v.Valor, v => new Monetario(v))
                .HasColumnName("Valor")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
