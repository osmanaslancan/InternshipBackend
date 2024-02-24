using AutoMapper;
using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using System.Security.Claims;

namespace InternshipBackend.Modules;

public interface IAccountService
{
    Task CreateAsync(CreateAccountDTO userInfo);
    Task<UserInfoDTO> GetCurrentUserInfoDTOAsync();
    Task UpdateUserInfo(UserInfoUpdateDTO userInfo);
}

public class AccountService(
    IAccountRepository accountRepository, 
    IHttpContextAccessor httpContextAccessor, 
    IValidator<CreateAccountDTO> createAccountDtoValidator, 
    IValidator<UserInfoUpdateDTO> userInfoUpdateDtoValidator,
    IMapper mapper) : IService, IAccountService
{
    public async Task CreateAsync(CreateAccountDTO userInfoDTO)
    {
        await createAccountDtoValidator.ValidateAndThrowAsync(userInfoDTO);

        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tokenEmail = httpContextAccessor.HttpContext!.User.FindFirstValue("email");

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

    public async Task<bool> HasProfile()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await accountRepository.GetBySupabaseIdAsync(userId);

        return user is not null;
    }

    private async Task<User> GetCurrentUserInfo()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userInfo = await accountRepository.GetBySupabaseIdAsync(userId) ?? throw new ValidationException("UserInfo not found for current user");
        return userInfo;
    }

    public async Task<UserInfoDTO> GetCurrentUserInfoDTOAsync()
    {
        var userInfo = await GetCurrentUserInfo();

        return new UserInfoDTO()
        {
            Name = userInfo.Name,
            Surname = userInfo.Surname,
            Email = userInfo.Email,
        };
    }

    public async Task UpdateUserInfo(UserInfoUpdateDTO newUserInfo)
    {
        var oldUserInfo = await GetCurrentUserInfo();

        await userInfoUpdateDtoValidator.ValidateAndThrowAsync(newUserInfo);

        var result = mapper.Map(newUserInfo, oldUserInfo);

        await accountRepository.UpdateAsync(result);
    }
}
