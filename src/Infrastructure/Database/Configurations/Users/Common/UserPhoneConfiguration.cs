using Domain.Features.Users.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Common
{
    public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("UserPhone");

            builder.HasKey(t => t.IdTelefone);
            builder.Property(t => t.IdTelefone).ValueGeneratedNever();

            builder.Property(t => t.IdUsuario).IsRequired();
            builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(20).IsRequired();
        }
    }
}
