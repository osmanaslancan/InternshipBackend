using InternshipBackend.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace InternshipBackend.Modules;

[Route("UserInfo/[action]")]
public class UserInfoEndpoint(IUserInfoService userInfoService) : ServiceEndpoint
{
    [Authorize, HttpPost]
    public async Task<EmptyResponse> CreateAsync(CreateUserInfoDTO userInfoDTO)
    {
        await userInfoService.CreateAsync(userInfoDTO);

        return new EmptyResponse();
    }

    [Authorize, HttpGet]
    public async Task<UserInfoDTO> Get()
    {
        var info = await userInfoService.GetCurrentUserInfoAsync();

        return info;
    }
}
