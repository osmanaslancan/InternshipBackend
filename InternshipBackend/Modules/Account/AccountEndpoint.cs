using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;

namespace InternshipBackend.Modules;

[Route("Account/[action]")]
public class AccountEndpoint(IAccountService accountService) : BaseEndpoint
{
    [Authorize, HttpPost]
    public async Task<ServiceResponse> UpdateUserInfo([FromBody] UserInfoUpdateDTO userInfo)
    {
        await accountService.UpdateUserInfo(userInfo);

        return new EmptyResponse();
    }

    [Authorize, HttpPost]
    public async Task<UserRegisteredResponse> IsUserRegistered()
    {
        var userInfo = await accountService.GetCurrentUserInfoOrDefault();

        return new UserRegisteredResponse()
        {
            IsRegistered = userInfo is not null,
        };
    }
    [Authorize, HttpPost]
    public async Task<ServiceResponse> UpdateProfileImage([FromForm] UpdateProfileImageRequest request)
    {
        await accountService.UpdateProfileImage(request);

        return new EmptyResponse();
    }
}
