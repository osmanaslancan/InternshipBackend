using AutoMapper;
using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using System.Security.Claims;

namespace InternshipBackend.Modules;

public interface IAccountService
{
    Task UpdateUserInfo(UserInfoUpdateDTO userInfo);
    Task<User?> GetCurrentUserInfoOrDefault();
}

public class AccountService(
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor,
    IValidator<UserInfoUpdateDTO> userInfoUpdateDtoValidator,
    IMapper mapper) : IService, IAccountService
{
    public async Task CreateAsync(UserInfoUpdateDTO userInfoDTO)
    {
        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tokenEmail = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(tokenEmail))
        {
            throw new ValidationException("Email not found in token");
        }

        if (await accountRepository.ExistsByEmail(tokenEmail))
        {
            throw new ValidationException("Email already exists");
        }

        if (await accountRepository.ExistsBySupabaseId(supabaseId))
        {
            throw new ValidationException("User already exists");
        }

        var userInfo = new User()
        {
            Name = userInfoDTO.Name,
            Email = tokenEmail,
            Surname = userInfoDTO.Surname,
            SupabaseId = supabaseId
        };

        await accountRepository.CreateAsync(userInfo);
    }

    public async Task<User?> GetCurrentUserInfoOrDefault()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await accountRepository.GetBySupabaseIdAsync(userId);

        return user;
    }

    //private async Task<User> GetCurrentUserInfo()
    //{
    //    var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    //    var userInfo = await accountRepository.GetBySupabaseIdAsync(userId) ?? throw new ValidationException("UserInfo not found for current user");
    //    return userInfo;
    //}

    public async Task UpdateUserInfo(UserInfoUpdateDTO newUserInfo)
    {
        var oldUserInfo = await GetCurrentUserInfoOrDefault();

        if (oldUserInfo is not null)
        {
            await userInfoUpdateDtoValidator.ValidateAndThrowAsync(newUserInfo);
            var result = mapper.Map(newUserInfo, oldUserInfo)!;
            await accountRepository.UpdateAsync(result);
        }

        await CreateAsync(newUserInfo);
    }
}
