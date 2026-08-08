using Domain.Features.Users.Common.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Users.Common
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.IdUsuario).ValueGeneratedNever();

            builder.Property(u => u.IdAcademia).IsRequired();

            builder.Property(u => u.Nome)
                .HasConversion(v => v.Valor, v => new Name(v))
                .HasColumnName("Nome")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(v => v.Valor, v => new Email(v))
                .HasColumnName("Email")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Senha).HasColumnName("Senha").IsRequired();
            builder.Property(u => u.DataNascimento).HasColumnName("DataNascimento").IsRequired();

            builder.Property(u => u.CPF)
                .HasConversion(v => v.Valor, v => new CPF(v))
                .HasColumnName("CPF")
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(u => u.TipoUsuario).HasColumnName("TipoUsuario").IsRequired();
            builder.Property(u => u.Ativo).HasColumnName("Ativo").IsRequired();
            builder.Property(u => u.Flag).HasColumnName("Flag").IsRequired();

            builder.Property(u => u.Quadra).HasColumnName("Quadra");
            builder.Property(u => u.Rua).HasColumnName("Rua");
            builder.Property(u => u.Bairro).HasColumnName("Bairro");
            builder.Property(u => u.Cidade).HasColumnName("Cidade");
            builder.Property(u => u.Estado).HasColumnName("Estado");
            builder.Property(u => u.CEP).HasColumnName("CEP");
        }
    }
}
