namespace InternshipBackend.Core.Authorization;

public interface IPermissionDefinitionProvider
{
    Task<PermissionDefinition?> GetPermissionAsync(string permissionName);
}