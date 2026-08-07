using Domain.Features.Token.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Token
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(r => r.IdRefreshToken);
            builder.Property(r => r.IdRefreshToken).ValueGeneratedNever();

            builder.Property(r => r.TokenHash).HasColumnName("TokenHash").HasMaxLength(500).IsRequired();
            builder.Property(r => r.DataExpiracao).HasColumnName("DataExpiracao").IsRequired();
            builder.Property(r => r.Revogado).HasColumnName("Revogado").IsRequired();
            builder.Property(r => r.IdUsuario).IsRequired();
        }
    }
}
