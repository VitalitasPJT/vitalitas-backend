using System.Collections.Generic;
using System.Text.Json;
using Domain.Features.Fichas.Ficha.Entities;
using Domain.Features.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Shared
{
    public class TreinoConfiguration : IEntityTypeConfiguration<Treino>
    {
        public void Configure(EntityTypeBuilder<Treino> builder)
        {
            builder.HasKey(e => e.IdTreino);
            builder.Property(e => e.NomeTreino).HasMaxLength(100);

            // Mantém o formato original (JSON livre em uma única coluna) em vez de normalizar
            // Exercicio em tabela própria — não existia tabela/entidade separada para isso.
            builder.Property(e => e.Exercicio)
                .HasConversion(
                    dict => JsonSerializer.Serialize(dict, (JsonSerializerOptions?)null),
                    json => JsonSerializer.Deserialize<Dictionary<string, List<Exercicio>>>(json, (JsonSerializerOptions?)null) ?? new())
                .HasColumnType("nvarchar(max)");

            builder.HasIndex(e => new { e.IdAcademia, e.IdTreino });
        }
    }
}
