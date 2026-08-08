using Domain.Features.Records.MedicalRecord.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Records.MedicalRecord
{
    public class MedicalRecordConfiguration : IEntityTypeConfiguration<Domain.Features.Records.MedicalRecord.Entities.MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Records.MedicalRecord.Entities.MedicalRecord> builder)
        {
            builder.ToTable("MedicalRecord");

            builder.HasKey(f => f.IdFicha);
            builder.Property(f => f.IdFicha).ValueGeneratedNever();

            builder.Property(f => f.IdAluno).IsRequired();
            builder.Property(f => f.Alergia).HasColumnName("Alergia").HasMaxLength(500);
            builder.Property(f => f.Restricao).HasColumnName("Restricao").HasMaxLength(500);
            builder.Property(f => f.Lesao).HasColumnName("Lesao").HasMaxLength(500);
            builder.Property(f => f.Cirurgia).HasColumnName("Cirurgia").HasMaxLength(500);
            builder.Property(f => f.ProblemaSaude).HasColumnName("ProblemaSaude").HasMaxLength(500);
            builder.Property(f => f.UsoMedicamento).HasColumnName("UsoMedicamento").HasMaxLength(500);
        }
    }
}
