using Domain.Features.Plans.Contract.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations.Plans.Contract
{
    public class ContractConfiguration : IEntityTypeConfiguration<Domain.Features.Plans.Contract.Entities.Contract>
    {
        public void Configure(EntityTypeBuilder<Domain.Features.Plans.Contract.Entities.Contract> builder)
        {
            builder.ToTable("Contract");

            builder.HasKey(c => c.IdContrato);
            builder.Property(c => c.IdContrato).ValueGeneratedNever();

            builder.Property(c => c.IdPlanoContrato).IsRequired();

            builder.Property(c => c.Mensalidade)
                .HasConversion(v => v.Valor, v => new Monetary(v))
                .HasColumnName("Mensalidade")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Status).HasColumnName("Status").IsRequired();
            builder.Property(c => c.DataFim).HasColumnName("DataFim").IsRequired();
            builder.Property(c => c.DataAssinatura).HasColumnName("DataAssinatura").IsRequired();
        }
    }
}
