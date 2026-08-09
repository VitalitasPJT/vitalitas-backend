using Domain.Features.Users.Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Manager
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Domain.Features.Users.Manager.Entities.Manager>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Users.Manager.Entities.Manager> builder)
        {
            builder.ToTable("Manager");

            builder.HasKey(g => g.IdGestor);
            builder.Property(g => g.IdGestor).ValueGeneratedNever();

            builder.Property(g => g.IdUsuario).IsRequired();
        }
    }
}
