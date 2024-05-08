using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.CompanyManagement;

[Authorize(PermissionKeys.CompanyOwner)]
[Route("Company")]
public class CompanyEndpoint(ICompanyService companyService) : BaseEndpoint
{
    [HttpPost("Update")]
    public async Task<ServiceResponse> UpdateAsync([FromBody] CompanyModifyDto dto)
    {
        await companyService.Upsert(dto);
        return new EmptyResponse();
    }
}