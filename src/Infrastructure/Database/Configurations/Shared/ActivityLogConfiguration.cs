using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLog");

            builder.HasKey(l => l.IdLog);
            builder.Property(l => l.IdLog).ValueGeneratedNever();

            builder.Property(l => l.IdUsuario).IsRequired();
            builder.Property(l => l.DataHora).HasColumnName("DataHora").IsRequired();
            builder.Property(l => l.Acao).HasColumnName("Acao").IsRequired();
            builder.Property(l => l.DispositivoLogado).HasColumnName("DispositivoLogado").HasMaxLength(300);
            builder.Property(l => l.Localizacao).HasColumnName("Localizacao").HasMaxLength(300);
        }
    }
}
