using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Warehouse.Shared.Services;

namespace Warehouse.Services;

public class ClaimService(IHttpContextAccessor _httpContextAccessor) : IClaimService
{
    public string GetUserId()
    {
        return GetClaim(ClaimTypes.NameIdentifier);
    }

    public string GetClaim(string key)
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value ?? string.Empty;
    }
}