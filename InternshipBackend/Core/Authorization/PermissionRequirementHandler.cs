using InternshipBackend.Modules.Account;
using Microsoft.AspNetCore.Authorization;

namespace InternshipBackend.Core.Authorization;

public class PermissionRequirementHandler(IAccountRepository accountRepository)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (await accountRepository.HasPermissionWithSupabaseId(context.User.GetSupabaseId(),
                requirement.PermissionName))
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}