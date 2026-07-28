using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(e => e.IdAluno);
            builder.Property(e => e.Objetivo).HasMaxLength(255);

            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => e.IdInstrutor);
            builder.HasIndex(e => new { e.IdAcademia, e.IdAluno });
        }
    }
}
