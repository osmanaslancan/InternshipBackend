using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.Internship;

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
    
    [HttpPost("Comment")]
    public async Task<ServiceResponse> CommentOnInternshipPostingAsync(InternshipCommentDto dto)
    {
        await internshipPostingService.CommentOnPosting(dto);
        return new EmptyResponse();
    }
}