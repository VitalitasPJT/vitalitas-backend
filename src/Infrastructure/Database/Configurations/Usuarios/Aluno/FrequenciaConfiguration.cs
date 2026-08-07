using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Aluno
{
    public class FrequenciaConfiguration : IEntityTypeConfiguration<Frequencia>
    {
        public void Configure(EntityTypeBuilder<Frequencia> builder)
        {
            builder.ToTable("Frequencia");

            builder.HasKey(f => f.IdFrequencia);
            builder.Property(f => f.IdFrequencia).ValueGeneratedNever();

            builder.Property(f => f.IdAluno).IsRequired();
            builder.Property(f => f.TempoTreinoMinutos).HasColumnName("TempoTreinoMinutos").IsRequired();
            builder.Property(f => f.Data).HasColumnName("Data").IsRequired();
        }
    }
}
