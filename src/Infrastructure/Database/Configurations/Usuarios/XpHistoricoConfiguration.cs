using Domain.Features.Usuarios.Aluno.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class XpHistoricoConfiguration : IEntityTypeConfiguration<XpHistorico>
    {
        public void Configure(EntityTypeBuilder<XpHistorico> builder)
        {
            builder.HasKey(e => e.IdXp);
            builder.Property(e => e.Motivo).HasMaxLength(255);
            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdXp });
        }
    }
}
