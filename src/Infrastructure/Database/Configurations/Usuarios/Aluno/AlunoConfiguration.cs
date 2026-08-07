using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Aluno
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Domain.Features.Usuarios.Aluno.Entities.Aluno>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Usuarios.Aluno.Entities.Aluno> builder)
        {
            builder.ToTable("Aluno");

            builder.HasKey(a => a.IdAluno);
            builder.Property(a => a.IdAluno).ValueGeneratedNever();

            builder.Property(a => a.IdUsuario).IsRequired();
            builder.Property(a => a.IdInstrutor).IsRequired();
            builder.Property(a => a.IdContrato).IsRequired();
            builder.Property(a => a.Objetivo).HasColumnName("Objetivo").HasMaxLength(500);
        }
    }
}
