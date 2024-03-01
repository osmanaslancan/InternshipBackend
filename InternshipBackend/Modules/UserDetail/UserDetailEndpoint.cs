using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("UserDetail/[action]")]
public class UserDetailEndpoint(IUserDetailService userDetailService) 
    : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync(UserDetailDTO userDetailDTO)
    {
        await userDetailService.UpdateAsync(userDetailDTO);
        return new EmptyResponse();
    }
}
