using Domain.Features.Academia.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Academia
{
    public class TelefoneAcademiaConfiguration : IEntityTypeConfiguration<TelefoneAcademia>
    {
        public void Configure(EntityTypeBuilder<TelefoneAcademia> builder)
        {
            builder.HasKey(e => e.IdTelefone);
            builder.Property(e => e.Telefone).HasMaxLength(20).IsRequired();
        }
    }
}
