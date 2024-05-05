using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UserDetail;

[Route("UserDetail")]
public class UserDetailEndpoint(IUserDetailService userDetailService) 
    : BaseEndpoint
{
    [HttpPost("Update")]
    public async Task<ServiceResponse> UpdateAsync([FromBody] UserDetailDto userDetailDto)
    {
        await userDetailService.Upsert(userDetailDto);
        return new EmptyResponse();
    }
}
