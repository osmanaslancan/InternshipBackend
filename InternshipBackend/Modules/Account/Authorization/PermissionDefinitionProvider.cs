using InternshipBackend.Core.Authorization;

namespace InternshipBackend.Modules.Account.Authorization;

public class PermissionDefinitionProvider : IPermissionDefinitionProvider
{
    private readonly Dictionary<string, PermissionDefinition> _permissionDefinitions = new()
    {
        { PermissionKeys.CompanyManagement, new PermissionDefinition(PermissionKeys.CompanyManagement) }
    };

    public Task<PermissionDefinition?> GetPermissionAsync(string permissionName)
    {
        return Task.FromResult(_permissionDefinitions.GetValueOrDefault(permissionName));
    }
}