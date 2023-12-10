using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using System.Security.Claims;

namespace InternshipBackend.Modules;

public interface IUserInfoService
{
    Task CreateAsync(CreateUserInfoDTO userInfo);
    Task<UserInfoDTO> GetCurrentUserInfoAsync();
}

public class UserInfoService(IUserInfoRepository UserInfoRepository, IHttpContextAccessor httpContextAccessor, IValidator<CreateUserInfoDTO> validator) : IService, IUserInfoService
{
    public async Task CreateAsync(CreateUserInfoDTO userInfoDTO)
    {
        await validator.ValidateAndThrowAsync(userInfoDTO);

        var userInfo = new UserInfo()
        {
            Name = userInfoDTO.Name,
            Email = userInfoDTO.Email,
            Surname = userInfoDTO.Surname,
            Age = userInfoDTO.Age,
            UniversityId = userInfoDTO.UniversityId,
            SupabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!),
        };

        await UserInfoRepository.CreateAsync(userInfo);
    }

    public async Task<UserInfoDTO> GetCurrentUserInfoAsync()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userInfo = await UserInfoRepository.GetByUserIdAsync(userId);

        if (userInfo is null)
        {
            throw new ValidationException("UserInfo not found for current user");
        }

        return new UserInfoDTO()
        {
            Name = userInfo.Name,
            Surname = userInfo.Surname,
            Email = userInfo.Email,
            Age = (int?)userInfo.Age,
            UniversityName = userInfo.University?.Name,
        };
    }
}
