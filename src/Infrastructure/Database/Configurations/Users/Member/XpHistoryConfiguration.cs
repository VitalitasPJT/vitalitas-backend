using Domain.Features.Users.Member.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Member
{
    public class XpHistoryConfiguration : IEntityTypeConfiguration<XpHistory>
    {
        public void Configure(EntityTypeBuilder<XpHistory> builder)
        {
            builder.ToTable("XpHistory");

            builder.HasKey(x => x.IdXp);
            builder.Property(x => x.IdXp).ValueGeneratedNever();

            builder.Property(x => x.IdUsuario).IsRequired();
            builder.Property(x => x.XpGanho).HasColumnName("XpGanho").IsRequired();
            builder.Property(x => x.Data).HasColumnName("Data").IsRequired();
            builder.Property(x => x.Motivo).HasColumnName("Motivo").HasMaxLength(500);
        }
    }
}
