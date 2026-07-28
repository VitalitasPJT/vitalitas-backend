using Domain.Features.Usuarios.Funcionario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(e => e.IdFuncionario);
            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => new { e.IdAcademia, e.IdFuncionario });
        }
    }
}
