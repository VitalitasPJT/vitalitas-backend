using Domain.Features.Token.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Token
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(e => e.IdRefreshToken);
            builder.Property(e => e.TokenHash).HasMaxLength(255).IsRequired();

            builder.HasIndex(e => e.TokenHash);
            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdRefreshToken });
        }
    }
}
