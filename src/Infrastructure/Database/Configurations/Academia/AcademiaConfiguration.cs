using Domain.Features.Academia.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Academia
{
    public class AcademiaConfiguration : IEntityTypeConfiguration<Domain.Features.Academia.Entities.Academia>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Academia.Entities.Academia> builder)
        {
            builder.HasKey(e => e.IdAcademia);

            builder.Property(e => e.NomeAcademia)
                .HasConversion<NomeConverter>()
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.CNPJ)
                .HasConversion<CnpjConverter>()
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(e => e.EmailInstitucional)
                .HasConversion<EmailConverter>()
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Quadra).HasMaxLength(100);
            builder.Property(e => e.Rua).HasMaxLength(100);
            builder.Property(e => e.Bairro).HasMaxLength(100);
            builder.Property(e => e.Cidade).HasMaxLength(100);
            builder.Property(e => e.Estado).HasMaxLength(100);
            builder.Property(e => e.CEP).HasMaxLength(8);
        }
    }
}
