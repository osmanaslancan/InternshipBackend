using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UserDetails;

[Route("UserDetail")]
[Authorize(PermissionKeys.Intern)]
public class UserDetailEndpoint(IUserDetailService userDetailService, IUserReferenceService userReferenceService) 
    : BaseEndpoint
{
    [HttpPost("Update")]
    public async Task<ServiceResponse> UpdateAsync([FromBody] UserDetailDto userDetailDto)
    {
        await userDetailService.Upsert(userDetailDto);
        return new EmptyResponse();
    }
    
    [HttpPost("Reference/Create")]
    public async Task<ServiceResponse> CreateAsync([FromBody] UserReferenceModifyDto dto)
    {
        await userReferenceService.CreateAsync(dto);
        return new EmptyResponse();
    }

    [HttpPost("Reference/Update/{id:int}")]
    public async Task<ServiceResponse> UpdateAsync([FromRoute] int id, [FromBody] UserReferenceModifyDto dto)
    {
        await userReferenceService.UpdateAsync(id, dto);
        return new EmptyResponse();
    }

    [HttpPost("Reference/Delete/{id:int}")]
    public async Task<ServiceResponse> DeleteAsync([FromRoute] int id)
    {
        await userReferenceService.DeleteAsync(id);
        return new EmptyResponse();
    }
}
