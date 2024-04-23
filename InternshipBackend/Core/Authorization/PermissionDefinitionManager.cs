namespace InternshipBackend.Core.Authorization;

public interface IPermissionDefinitionManager
{
    Task<string?> GetPermissionNameForPolicyOrNullAsync(string policy);
}

public class PermissionDefinitionManager : IPermissionDefinitionManager
{
    private readonly IServiceProvider serviceProvider;

    public PermissionDefinitionManager(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<string?> GetPermissionNameForPolicyOrNullAsync(string policy)
    {
        var providers = serviceProvider.GetRequiredService<IEnumerable<IPermissionDefinitionProvider>>();
        foreach (var provider in providers)
        {
            var definition = await provider.GetPermissionAsync(policy);
            if (definition is not null)
            {
                return definition.Name;
            }
        }

        return null;
    }
}

