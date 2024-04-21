using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.Account;

[Route("Account/[action]")]
public class AccountEndpoint(IAccountService accountService) : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> UpdateUserInfo([FromBody] UserInfoUpdateDto userInfo)
    {
        await accountService.UpdateUserInfo(userInfo);

        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<UserRegisteredResponse> IsUserRegistered()
    {
        var userInfo = await accountService.GetCurrentUserInfoOrDefault();

        return new UserRegisteredResponse()
        {
            IsRegistered = userInfo is not null,
        };
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateProfileImage([FromForm] UpdateProfileImageRequest request)
    {
        await accountService.UpdateProfileImage(request);

        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse<UserDTO>> GetInfo()
    {
        var userDto = await accountService.GetUser();

        return new ServiceResponse<UserDTO>()
        {
            Data = userDto,
        };
    }
}
