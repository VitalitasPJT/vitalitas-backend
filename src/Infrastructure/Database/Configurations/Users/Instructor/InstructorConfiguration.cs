using Domain.Features.Users.Instructor.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Instructor
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Domain.Features.Users.Instructor.Entities.Instructor>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Users.Instructor.Entities.Instructor> builder)
        {
            builder.ToTable("Instructor");

            builder.HasKey(i => i.IdInstrutor);
            builder.Property(i => i.IdInstrutor).ValueGeneratedNever();

            builder.Property(i => i.IdUsuario).IsRequired();

            builder.Property(i => i.CREF)
                .HasConversion(v => v.Valor, v => new CREF(v))
                .HasColumnName("CREF")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
