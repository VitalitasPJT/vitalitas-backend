using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Aluno
{
    public class XpHistoricoConfiguration : IEntityTypeConfiguration<XpHistorico>
    {
        public void Configure(EntityTypeBuilder<XpHistorico> builder)
        {
            builder.ToTable("XpHistorico");

            builder.HasKey(x => x.IdXp);
            builder.Property(x => x.IdXp).ValueGeneratedNever();

            builder.Property(x => x.IdUsuario).IsRequired();
            builder.Property(x => x.XpGanho).HasColumnName("XpGanho").IsRequired();
            builder.Property(x => x.Data).HasColumnName("Data").IsRequired();
            builder.Property(x => x.Motivo).HasColumnName("Motivo").HasMaxLength(500);
        }
    }
}
