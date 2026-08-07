using Domain.Features.Usuarios.Funcionario.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Usuarios.Funcionario
{
    public class FuncionarioConfiguration : IEntityTypeConfiguration<Domain.Features.Usuarios.Funcionario.Entities.Funcionario>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Usuarios.Funcionario.Entities.Funcionario> builder)
        {
            builder.ToTable("Funcionario");

            builder.HasKey(f => f.IdFuncionario);
            builder.Property(f => f.IdFuncionario).ValueGeneratedNever();

            builder.Property(f => f.IdUsuario).IsRequired();

            // Nota: FuncionarioRepository.CriarFuncionario (Dapper) grava Cargo como texto
            // (cargo.ToString()), enquanto o restante do sistema trata enums como int
            // (ex.: Usuario.TipoUsuario). Mantido como int aqui por consistência com o
            // resto do schema; esse descompasso no repository é pré-existente e fica
            // como item separado a alinhar.
            builder.Property(f => f.Cargo).HasColumnName("Cargo").IsRequired();
        }
    }
}
