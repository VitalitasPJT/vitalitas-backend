using Domain.Features.Usuarios.Administrador.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class AdministradorConfiguration : IEntityTypeConfiguration<Administrador>
    {
        public void Configure(EntityTypeBuilder<Administrador> builder)
        {
            builder.HasKey(e => e.IdFuncionario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdFuncionario });
        }
    }
}
