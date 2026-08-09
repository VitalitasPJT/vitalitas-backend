using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedule");

            builder.HasKey(a => a.IdAgenda);
            builder.Property(a => a.IdAgenda).ValueGeneratedOnAdd();

            builder.Property(a => a.IdInstrutor).IsRequired();
            builder.Property(a => a.IdAcademia).IsRequired();
            builder.Property(a => a.Status).HasColumnName("Status").IsRequired();
            builder.Property(a => a.Data).HasColumnName("Data").IsRequired();
        }
    }
}
