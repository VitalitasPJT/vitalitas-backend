using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class FrequenciaConfiguration : IEntityTypeConfiguration<Frequencia>
    {
        public void Configure(EntityTypeBuilder<Frequencia> builder)
        {
            builder.HasKey(e => e.IdFrequencia);
            builder.HasIndex(e => e.IdAluno);
            builder.HasIndex(e => new { e.IdAcademia, e.IdFrequencia });
        }
    }
}
