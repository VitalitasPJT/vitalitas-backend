using Domain.Features.Usuarios.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Common
{
    public class TelefoneUsuarioConfiguration : IEntityTypeConfiguration<TelefoneUsuario>
    {
        public void Configure(EntityTypeBuilder<TelefoneUsuario> builder)
        {
            builder.ToTable("TelefoneUsuario");

            builder.HasKey(t => t.IdTelefone);
            builder.Property(t => t.IdTelefone).ValueGeneratedNever();

            builder.Property(t => t.IdUsuario).IsRequired();
            builder.Property(t => t.Telefone).HasColumnName("Telefone").HasMaxLength(20).IsRequired();
        }
    }
}
