using System.Diagnostics;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Modules.Account;
using Microsoft.AspNetCore.Authorization;

namespace InternshipBackend.Core.Authorization;

public class UserTypeRequirementHandler(IAccountRepository accountRepository)
    : AuthorizationHandler<UserTypeRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        UserTypeRequirement requirement)
    {
        var accountType = requirement.PermissionName switch
        {
            "CompanyOwner" => AccountType.CompanyOwner,
            "Intern" => AccountType.Intern,
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