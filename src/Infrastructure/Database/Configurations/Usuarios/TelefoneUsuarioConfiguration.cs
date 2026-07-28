using Domain.Features.Usuarios.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class TelefoneUsuarioConfiguration : IEntityTypeConfiguration<TelefoneUsuario>
    {
        public void Configure(EntityTypeBuilder<TelefoneUsuario> builder)
        {
            builder.HasKey(e => e.IdTelefone);
            builder.Property(e => e.Telefone).HasMaxLength(20).IsRequired();
            builder.HasIndex(e => new { e.IdAcademia, e.IdTelefone });
        }
    }
}
