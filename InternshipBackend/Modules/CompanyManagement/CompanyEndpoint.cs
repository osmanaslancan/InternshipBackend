using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace InternshipBackend.Modules.CompanyManagement;

[Authorize(PermissionKeys.CompanyOwner)]
public class CompanyEndpoint : BaseEndpoint
{
    
}