using Microsoft.AspNetCore.Authorization;

namespace InternshipBackend.Core.Authorization;

public class UserTypeRequirement : IAuthorizationRequirement
{
    public UserTypeRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }

    public string PermissionName { get; }
}
