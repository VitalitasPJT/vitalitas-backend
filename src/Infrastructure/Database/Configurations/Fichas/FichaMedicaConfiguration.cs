using Domain.Features.Fichas.FichaMedica.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Fichas
{
    public class FichaMedicaConfiguration : IEntityTypeConfiguration<FichaMedica>
    {
        public void Configure(EntityTypeBuilder<FichaMedica> builder)
        {
            builder.HasKey(e => e.IdFicha);
            builder.Property(e => e.Alergia).HasMaxLength(100);
            builder.Property(e => e.Restricao).HasMaxLength(255);
            builder.Property(e => e.Lesao).HasMaxLength(255);
            builder.Property(e => e.Cirurgia).HasMaxLength(255);
            builder.Property(e => e.ProblemaSaude).HasMaxLength(255);
            builder.Property(e => e.UsoMedicamento).HasMaxLength(255);
            builder.HasIndex(e => e.IdAluno);
            builder.HasIndex(e => new { e.IdAcademia, e.IdFicha });
        }
    }
}
