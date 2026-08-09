using Domain.Features.Users.Member.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Member
{
    public class MemberConfiguration : IEntityTypeConfiguration<Domain.Features.Users.Member.Entities.Member>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Users.Member.Entities.Member> builder)
        {
            builder.ToTable("Member");

            builder.HasKey(a => a.IdAluno);
            builder.Property(a => a.IdAluno).ValueGeneratedNever();

            builder.Property(a => a.IdUsuario).IsRequired();
            builder.Property(a => a.IdInstrutor).IsRequired();
            builder.Property(a => a.IdContrato).IsRequired();
            builder.Property(a => a.Objetivo).HasColumnName("Objetivo").HasMaxLength(500);
        }
    }
}
