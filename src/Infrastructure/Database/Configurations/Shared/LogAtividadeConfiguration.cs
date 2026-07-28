using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class LogAtividadeConfiguration : IEntityTypeConfiguration<LogAtividade>
    {
        public void Configure(EntityTypeBuilder<LogAtividade> builder)
        {
            builder.HasKey(e => e.IdLog);
            builder.Property(e => e.DispositivoLogado).HasMaxLength(255);
            builder.Property(e => e.Localizacao).HasMaxLength(255);

            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdLog });
        }
    }
}
