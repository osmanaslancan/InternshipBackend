using AutoMapper;
using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Data;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
namespace InternshipBackend.Modules;

public interface IAccountService
{
    Task UpdateUserInfo(UserInfoUpdateDTO userInfo);
    Task<User?> GetCurrentUserInfoOrDefault();
    Task UpdateProfileImage(UpdateProfileImageRequest request);
}

public class AccountService(
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor,
    IValidator<UserInfoUpdateDTO> userInfoUpdateDtoValidator,
    IHttpClientFactory clientFactory,
    IConfiguration configuration,
    IMapper mapper) : IService, IAccountService
{
    public async Task CreateAsync(UserInfoUpdateDTO userInfoDTO)
    {
        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var tokenEmail = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(tokenEmail))
        {
            throw new ValidationException(ErrorCodes.EmailNotFound);
        }

        if (await accountRepository.ExistsByEmail(tokenEmail))
        {
            throw new ValidationException(ErrorCodes.EmailExists);
        }

        if (await accountRepository.ExistsBySupabaseId(supabaseId))
        {
            throw new ValidationException(ErrorCodes.UserAlreadyExists);
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
            return;
        }

        await CreateAsync(newUserInfo);
    }

    public async Task UpdateProfileImage(UpdateProfileImageRequest request)
    {
        var user = await GetCurrentUserInfoOrDefault() ?? throw new ValidationException(ErrorCodes.UserNotFound);

        using var image = SixLabors.ImageSharp.Image.Load(request.Image.OpenReadStream());
        image.Mutate(x => x.Resize(256, 256));
        using var resultStream = new MemoryStream();
        image.Save(resultStream, new PngEncoder());
        
        var content = new MultipartFormDataContent();
        var streamContent = new ByteArrayContent(resultStream.ToArray());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(request.Image.ContentType);
        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = request.Image.Name,
            FileName = request.Image.FileName,
        };
        content.Add(streamContent);
        content.Headers.Add("X-Upsert", "true");
        using var client = clientFactory.CreateClient("Supabase");
        var response = await client.PostAsync($"{configuration["SupabaseStorageBaseUrl"]}/PublicProfilePhoto/{user.SupabaseId}/ProfilePhoto", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload image");
        }

        user.ProfilePhotoUrl = $"{configuration["SupabaseStorageBaseUrl"]}/public/PublicProfilePhoto/{user.SupabaseId}/ProfilePhoto";
        await accountRepository.UpdateAsync(user);
    }
}
