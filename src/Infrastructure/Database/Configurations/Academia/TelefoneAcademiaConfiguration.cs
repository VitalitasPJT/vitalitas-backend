using Domain.Features.Academia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Academia
{
    public class TelefoneAcademiaConfiguration : IEntityTypeConfiguration<TelefoneAcademia>
    {
        public void Configure(EntityTypeBuilder<TelefoneAcademia> builder)
        {
            builder.ToTable("TelefoneAcademia");

            builder.HasKey(t => t.IdTelefone);
            builder.Property(t => t.IdTelefone).ValueGeneratedNever();

            builder.Property(t => t.IdAcademia).IsRequired();
            builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(20).IsRequired();
        }
    }
}
