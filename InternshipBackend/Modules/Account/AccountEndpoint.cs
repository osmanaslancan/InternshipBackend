using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.UniversityEducations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.Account;

[Route("Account/[action]")]
public class AccountEndpoint(IAccountService accountService, ILinkedinScraperService linkedinScraperService) : BaseEndpoint
{
    [HttpPost]
    public async Task<LinkedinScrapeResponse> ScrapeLinkedin([FromBody] LinkedinScrapeRequest request)
    {
        var result = await linkedinScraperService.ScrapeLinkedin(request);
        return result;
    }

    
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

public class LinkedinScrapeRequest
{
    public required string AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}
