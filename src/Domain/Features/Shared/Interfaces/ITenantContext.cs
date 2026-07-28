namespace Domain.Features.Shared.Interfaces
{
    public interface ITenantContext
    {
        Guid TenantId { get; }
    }
}
