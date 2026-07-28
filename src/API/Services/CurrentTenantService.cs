using Domain.Features.Shared.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class CurrentTenantService : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentTenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid TenantId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User.FindFirst("TenantId")?.Value;
                if (string.IsNullOrWhiteSpace(claim) || !Guid.TryParse(claim, out var tenantId))
                    throw new UnauthorizedAccessException("Tenant não identificado na requisição.");

                return tenantId;
            }
        }
    }
}
