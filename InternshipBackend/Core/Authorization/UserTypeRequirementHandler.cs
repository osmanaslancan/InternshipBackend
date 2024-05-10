using System.Diagnostics;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace InternshipBackend.Core.Authorization;

public class UserTypeRequirementHandler(IAccountRepository accountRepository)
    : AuthorizationHandler<UserTypeRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        UserTypeRequirement requirement)
    {
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            context.Fail();
            return;
        }
        
        if (requirement.PermissionName == PermissionKeys.Common)
        {
            context.Succeed(requirement);
            return;
        }
        
        var accountType = requirement.PermissionName switch
        {
            PermissionKeys.CompanyOwner => AccountType.CompanyOwner,
            PermissionKeys.Intern => AccountType.Intern,
            _ => throw new NotImplementedException()
        };
        
        if (await accountRepository.HasTypeWithSupabaseId(context.User.GetSupabaseId(),
                accountType))
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}