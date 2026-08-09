using Domain.Features.Gym.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Gym
{
    public class GymConfiguration : IEntityTypeConfiguration<Domain.Features.Gym.Entities.Gym>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Gym.Entities.Gym> builder)
        {
            builder.ToTable("Gym");

            builder.HasKey(a => a.IdAcadenia);
            builder.Property(a => a.IdAcadenia).ValueGeneratedNever();

            builder.Property(a => a.IdLicenca).IsRequired();
            builder.Property(a => a.IdGestor).IsRequired();

            builder.Property(a => a.NomeAcademia)
                .HasConversion(v => v.Valor, v => new Name(v))
                .HasColumnName("NomeAcademia")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.CNPJ)
                .HasConversion(v => v.Valor, v => new CNPJ(v))
                .HasColumnName("CNPJ")
                .HasMaxLength(18)
                .IsRequired();

            builder.Property(a => a.Quadra).HasColumnName("Quadra");
            builder.Property(a => a.Rua).HasColumnName("Rua");
            builder.Property(a => a.Bairro).HasColumnName("Bairro");
            builder.Property(a => a.Cidade).HasColumnName("Cidade");
            builder.Property(a => a.Estado).HasColumnName("Estado");
            builder.Property(a => a.CEP).HasColumnName("CEP");
            builder.Property(a => a.TipoAcademia).HasColumnName("TipoAcademia").IsRequired();

            builder.Property(a => a.EmailInstitucional)
                .HasConversion(v => v.Valor, v => new Email(v))
                .HasColumnName("EmailInstitucional")
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
