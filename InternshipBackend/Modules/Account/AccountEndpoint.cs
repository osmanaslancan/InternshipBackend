using InternshipBackend.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace InternshipBackend.Modules;

[Route("Account/[action]")]
public class AccountEndpoint(IAccountService accountService) : ServiceEndpoint
{
    [Authorize, HttpPost]
    public async Task<EmptyResponse> RegisterAsync(CreateAccountDTO userInfoDTO)
    {
        await accountService.CreateAsync(userInfoDTO);

        return new EmptyResponse();
    }

    [Authorize, HttpGet]
    public async Task<ServiceResponse<UserInfoDTO>> Get()
    {
        var info = await accountService.GetCurrentUserInfoDTOAsync();

        return ServiceResponse.Success(info);
    }

    [Authorize, HttpPost]
    public async Task<EmptyResponse> UpdateUserInfo(UserInfoUpdateDTO userInfo)
    {
        await accountService.UpdateUserInfo(userInfo);

        return new EmptyResponse();
    }
}
