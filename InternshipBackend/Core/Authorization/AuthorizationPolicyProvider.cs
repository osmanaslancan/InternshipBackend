using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace InternshipBackend.Core.Authorization;

public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, IAuthorizationPolicyProvider
{
    private readonly IOptions<AuthorizationOptions> options;
    private readonly IPermissionDefinitionManager permissionDefinitionManager;

    public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IPermissionDefinitionManager permissionDefinitionManager) 
        : base(options)
    {
        this.options = options;
        this.permissionDefinitionManager = permissionDefinitionManager;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy != null)
        {
            return policy;
        }


        var permission = await permissionDefinitionManager.GetPermissionNameForPolicyOrNullAsync(policyName);
        if (permission != null)
        {
            var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
            
            if (permission.Type == PermissionRequirementType.Database)
                policyBuilder.Requirements.Add(new PermissionRequirement(policyName));
            else if (permission.Type == PermissionRequirementType.UserType)
                policyBuilder.Requirements.Add(new UserTypeRequirement(policyName));
            
            return policyBuilder.Build();
        }

        return null;
    }
}
