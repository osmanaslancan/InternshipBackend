using System.Security.Claims;
using System.Text.Json;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Core;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetSupabaseId(this ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    }


    public static AccountType? GetUserType(this ClaimsPrincipal claimsPrincipal)
    {
        var data = claimsPrincipal?.FindFirstValue("app_metadata");
        if (data is null)
        {
            return null;            
        }

        var appMetadata = JsonSerializer.Deserialize<Dictionary<string, object>>(data);

        int? userType =
            appMetadata?.GetValueOrDefault("user_type") is JsonElement
            {
                ValueKind: JsonValueKind.Number
            } element
                ? element.GetInt32()
                : null;
        
        return (AccountType?)userType;
    }
    
    public static bool IsIntern(this ClaimsPrincipal claimsPrincipal)
    {
        return GetUserType(claimsPrincipal) == AccountType.Intern;
    }
    
    public static bool IsCompanyOwner(this ClaimsPrincipal claimsPrincipal)
    {
        return GetUserType(claimsPrincipal) == AccountType.CompanyOwner;
    }
}
