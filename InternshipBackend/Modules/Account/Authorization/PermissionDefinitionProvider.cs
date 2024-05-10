using InternshipBackend.Core.Authorization;

namespace InternshipBackend.Modules.Account.Authorization;

public class PermissionDefinitionProvider : IPermissionDefinitionProvider
{
    private readonly Dictionary<string, PermissionDefinition> _permissionDefinitions = new()
    {
        { PermissionKeys.CompanyOwner, new PermissionDefinition(PermissionKeys.CompanyOwner, PermissionRequirementType.UserType) },
        { PermissionKeys.Intern, new PermissionDefinition(PermissionKeys.Intern, PermissionRequirementType.UserType) },
        { PermissionKeys.Common, new PermissionDefinition(PermissionKeys.Common, PermissionRequirementType.UserType) }
    };

    public Task<PermissionDefinition?> GetPermissionAsync(string permissionName)
    {
        return Task.FromResult(_permissionDefinitions.GetValueOrDefault(permissionName));
    }
}