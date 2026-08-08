using Domain.Features.Gym.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Gym
{
    public class GymPhoneConfiguration : IEntityTypeConfiguration<GymPhone>
    {
        public void Configure(EntityTypeBuilder<GymPhone> builder)
        {
            builder.ToTable("GymPhone");

            builder.HasKey(t => t.IdTelefone);
            builder.Property(t => t.IdTelefone).ValueGeneratedNever();

            builder.Property(t => t.IdAcademia).IsRequired();
            builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(20).IsRequired();
        }
    }
}
