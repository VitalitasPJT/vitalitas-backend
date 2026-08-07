using Domain.Features.Planos.Licenca.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Planos.Licenca
{
    public class PlanoLicencaConfiguration : IEntityTypeConfiguration<PlanoLicenca>
    {
        public void Configure(EntityTypeBuilder<PlanoLicenca> builder)
        {
            builder.ToTable("PlanoLicenca");

            builder.HasKey(p => p.IdPlanoLicenca);
            builder.Property(p => p.IdPlanoLicenca).ValueGeneratedNever();

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
