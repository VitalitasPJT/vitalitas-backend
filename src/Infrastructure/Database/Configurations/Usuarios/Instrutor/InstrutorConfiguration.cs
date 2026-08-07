using Domain.Features.Usuarios.Instrutor.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Instrutor
{
    public class InstrutorConfiguration : IEntityTypeConfiguration<Domain.Features.Usuarios.Instrutor.Entities.Instrutor>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Usuarios.Instrutor.Entities.Instrutor> builder)
        {
            builder.ToTable("Instrutor");

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
