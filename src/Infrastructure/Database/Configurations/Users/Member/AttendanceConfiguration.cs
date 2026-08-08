using Domain.Features.Users.Member.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Member
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("Attendance");

            builder.HasKey(f => f.IdFrequencia);
            builder.Property(f => f.IdFrequencia).ValueGeneratedNever();

            builder.Property(f => f.IdAluno).IsRequired();
            builder.Property(f => f.TempoTreinoMinutos).HasColumnName("TempoTreinoMinutos").IsRequired();
            builder.Property(f => f.Data).HasColumnName("Data").IsRequired();
        }
    }
}
