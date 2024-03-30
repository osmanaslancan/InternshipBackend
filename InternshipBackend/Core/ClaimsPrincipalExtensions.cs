using System.Security.Claims;

namespace InternshipBackend.Core;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetSupabaseId(this ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }
}
