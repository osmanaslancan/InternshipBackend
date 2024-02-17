using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using System.Security.Claims;

namespace InternshipBackend.Modules;

public interface IAccountService
{
    Task CreateAsync(CreateAccountDTO userInfo);
    Task<UserInfoDTO> GetCurrentUserInfoAsync();
}

public class AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IValidator<CreateAccountDTO> validator) : IService, IAccountService
{
    public async Task CreateAsync(CreateAccountDTO userInfoDTO)
    {
        await validator.ValidateAndThrowAsync(userInfoDTO);

        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tokenEmail = httpContextAccessor.HttpContext!.User.FindFirstValue("email");

        if (await accountRepository.ExistsByEmail(userInfoDTO.Email))
        {
            throw new ValidationException("Email already exists");
        }

        if (await accountRepository.ExistsBySupabaseId(supabaseId))
        {
            throw new ValidationException("User already exists");
        }

        if (tokenEmail != userInfoDTO.Email)
        {
            throw new ValidationException("Name specified in token should match the given email");
        }

        var userInfo = new UserInfo()
        {
            Name = userInfoDTO.Name,
            Email = userInfoDTO.Email,
            Surname = userInfoDTO.Surname,
            SupabaseId = supabaseId
        };

        await accountRepository.CreateAsync(userInfo);
    }

    public async Task<UserInfoDTO> GetCurrentUserInfoAsync()
    {
        var userId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var userInfo = await accountRepository.GetBySupabaseIdAsync(userId);

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
