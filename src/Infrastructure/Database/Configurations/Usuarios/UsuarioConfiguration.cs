using Domain.Features.Usuarios.Common.Entities;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.IdUsuario);

            builder.Property(e => e.Nome)
                .HasConversion<NomeConverter>()
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasConversion<EmailConverter>()
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.CPF)
                .HasConversion<CpfConverter>()
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(e => e.Senha).HasMaxLength(255).IsRequired();
            builder.Property(e => e.Quadra).HasMaxLength(100);
            builder.Property(e => e.Rua).HasMaxLength(100);
            builder.Property(e => e.Bairro).HasMaxLength(100);
            builder.Property(e => e.Cidade).HasMaxLength(100);
            builder.Property(e => e.Estado).HasMaxLength(100);
            builder.Property(e => e.CEP).HasMaxLength(8);

            // Mesmas constraints únicas do CREATE.sql original.
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.CPF).IsUnique();
            builder.HasIndex(e => new { e.IdAcademia, e.IdUsuario });
        }
    }
}
