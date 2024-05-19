using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.Internship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.Account;

[Route("Account")]
public class AccountEndpoint(IAccountService accountService) : BaseEndpoint
{
    [HttpPost("UpdateUserInfo")]
    public async Task<ServiceResponse> UpdateUserInfo([FromBody] UserInfoUpdateDto userInfo)
    {
        await accountService.UpdateUserInfo(userInfo);

        return new EmptyResponse();
    }

    [HttpPost("IsUserRegistered")]
    public async Task<UserRegisteredResponse> IsUserRegistered()
    {
        var userInfo = await accountService.GetCurrentUserInfoOrDefault();

        return new UserRegisteredResponse()
        {
            IsRegistered = userInfo is not null,
        };
    }

    [HttpPost("UpdateProfileImage")]
    public async Task<ServiceResponse> UpdateProfileImage([FromForm] UpdateProfileImageRequest request)
    {
        await accountService.UpdateProfileImage(request);

        return new EmptyResponse();
    }

    [HttpPost("GetInfo")]
    public async Task<ServiceResponse<UserDTO>> GetInfo()
    {
        var userDto = await accountService.GetUser();

        return new ServiceResponse<UserDTO>()
        {
            Data = userDto,
        };
    }

    [HttpPost("Follow/Company/{companyId:int}")]
    public async Task<ServiceResponse> FollowCompany([FromRoute] int companyId, [FromQuery] bool follow)
    {
        await accountService.FollowCompany(companyId, follow);

        return new EmptyResponse();
    }

    [HttpPost("Follow/Posting/{postingId:int}")]
    public async Task<ServiceResponse> FollowPosting([FromRoute] int postingId, [FromQuery] bool follow)
    {
        await accountService.FollowPosting(postingId, follow);

        return new EmptyResponse();
    }
    
    [HttpGet("ListApplications")]
    [Authorize(PermissionKeys.Intern)]
    public async Task<ServiceResponse<List<InternshipApplicationInternListDto>>> ListApplications()
    {
        var result = await accountService.ListApplications();

        return new ServiceResponse<List<InternshipApplicationInternListDto>>()
        {
            Data = result,
        };
    }

    [HttpPost("RegisterNotificationToken")]
    public async Task<ServiceResponse> RegisterNotificationToken([FromBody] RegisterNotificationTokenDto request)
    {
        await accountService.RegisterNotificationToken(request);

        return new EmptyResponse();
    }

    [HttpGet("Messages")]
    public async Task<ServiceResponse<List<UserNotificationDto>>> Messages()
    {
        var result = await accountService.GetCurrentUserMessages();

        return new ServiceResponse<List<UserNotificationDto>>
        {
            Data = result
        };
    }
}