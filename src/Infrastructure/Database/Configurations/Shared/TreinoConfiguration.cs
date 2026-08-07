using System.Text.Json;
using Domain.Features.Fichas.Ficha.Entities;
using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class TreinoConfiguration : IEntityTypeConfiguration<Treino>
    {
        public void Configure(EntityTypeBuilder<Treino> builder)
        {
            builder.ToTable("Treino");

            builder.HasKey(t => t.IdTreino);
            builder.Property(t => t.IdTreino).ValueGeneratedNever();

            builder.Property(t => t.IdFicha).IsRequired();
            builder.Property(t => t.Tipo).HasColumnName("Tipo").IsRequired();
            builder.Property(t => t.NomeTreino).HasColumnName("NomeTreino").HasMaxLength(200);

            // Serializado como JSON em uma única coluna, conforme já indicado pelo
            // comentário original da entidade Treino ("converter para o banco").
            var exercicioComparer = new ValueComparer<Dictionary<string, List<Exercicio>>>(
                (a, b) => JsonSerializer.Serialize(a, (JsonSerializerOptions?)null) == JsonSerializer.Serialize(b, (JsonSerializerOptions?)null),
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null).GetHashCode(),
                v => JsonSerializer.Deserialize<Dictionary<string, List<Exercicio>>>(
                    JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), (JsonSerializerOptions?)null)!);

            builder.Property(t => t.Exercicio)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<Dictionary<string, List<Exercicio>>>(v, (JsonSerializerOptions?)null) ?? new())
                .Metadata.SetValueComparer(exercicioComparer);

            builder.Property(t => t.Exercicio).HasColumnName("Exercicio").HasColumnType("nvarchar(max)");
        }
    }
}
