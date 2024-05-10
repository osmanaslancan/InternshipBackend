using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.CompanyManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.InternshipManagement;

[Authorize(PermissionKeys.Intern)]
[Route("Internship")]
public class InternshipManagementEndpoint(IInternshipPostingService internshipPostingService) : BaseEndpoint
{
    
    [HttpPost("Apply")]
    public async Task<ServiceResponse> ApplyToInternshipPostingAsync(InternshipApplicationDto dto)
    {
        await internshipPostingService.ApplyToPosting(dto);
        return new EmptyResponse();
    }
}