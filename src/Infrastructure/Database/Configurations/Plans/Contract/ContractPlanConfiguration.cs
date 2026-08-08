using Domain.Features.Plans.Contract.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Plans.Contract
{
    public class ContractPlanConfiguration : IEntityTypeConfiguration<ContractPlan>
    {
        public void Configure(EntityTypeBuilder<ContractPlan> builder)
        {
            builder.ToTable("ContractPlan");

            builder.HasKey(p => p.IdPlano);
            builder.Property(p => p.IdPlano).ValueGeneratedNever();

            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(200).IsRequired();
            builder.Property(p => p.Descricao).HasColumnName("Descricao").HasMaxLength(1000);

            builder.Property(p => p.Valor)
                .HasConversion(v => v.Valor, v => new Monetary(v))
                .HasColumnName("Valor")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
