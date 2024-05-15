using System.Net.Http.Headers;
using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using FluentValidation.Validators;
using InternshipBackend.Core;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace InternshipBackend.Modules.Account;

public interface IAccountService
{
    Task UpdateUserInfo(UserInfoUpdateDto dto);
    Task<User?> GetCurrentUserInfoOrDefault();
    Task UpdateProfileImage(UpdateProfileImageRequest request);
    Task<UserDTO> GetUser();
    Task FollowCompany(int companyId, bool follow);
    Task FollowPosting(int postingId, bool follow);
}

public class AccountService(
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor,
    IValidator<UserInfoUpdateDto> userInfoUpdateDtoValidator,
    IHttpClientFactory clientFactory,
    IConfiguration configuration,
    IMapper mapper) : IScopedService, IAccountService
{
    private async Task<(Guid supabaseId, string email)> GetSupabaseIdAndEmail()
    {
        var supabaseId = httpContextAccessor.HttpContext!.User.GetSupabaseId()!;
        var email = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;

        if (string.IsNullOrEmpty(email))
        {
            throw new ValidationException(ErrorCodes.EmailNotFound);
        }

        if (await accountRepository.ExistsByEmail(email))
        {
            throw new ValidationException(ErrorCodes.EmailExists);
        }

        if (await accountRepository.ExistsBySupabaseId(supabaseId))
        {
            throw new ValidationException(ErrorCodes.UserAlreadyExists);
        }

        return (supabaseId, email);
    }

    private async Task CreateAsync(UserInfoUpdateDto userInfoDto)
    {
        var (supabaseId, tokenEmail) = await GetSupabaseIdAndEmail();

        var userInfo = new User()
        {
            Name = userInfoDto.Name,
            Email = tokenEmail,
            Surname = userInfoDto.Surname,
            SupabaseId = supabaseId,
            PhoneNumber = userInfoDto.PhoneNumber,
            AccountType = userInfoDto.AccountType!.Value,
        };

        await accountRepository.CreateAsync(userInfo);
    }

    public async Task<User?> GetCurrentUserInfoOrDefault()
    {
        var userId = httpContextAccessor.HttpContext!.User.GetSupabaseId()!;
        var user = await accountRepository.GetBySupabaseIdAsync(userId);

        return user;
    }

    public async Task UpdateUserInfo(UserInfoUpdateDto dto)
    {
        var currentUserInfo = await GetCurrentUserInfoOrDefault();


        if (currentUserInfo is not null)
        {
            await userInfoUpdateDtoValidator.ValidateAsync(dto, o =>
            {
                o.ThrowOnFailures();
                o.IncludeRuleSets("Update");
            });
            currentUserInfo.Name = dto.Name;
            currentUserInfo.Surname = dto.Surname;
            currentUserInfo.PhoneNumber = dto.PhoneNumber;

            await accountRepository.UpdateAsync(currentUserInfo);
            return;
        }

        await userInfoUpdateDtoValidator.ValidateAsync(dto, o =>
        {
            o.ThrowOnFailures();
            o.IncludeRuleSets("Create");
        });

        await CreateAsync(dto);
    }

    public async Task UpdateProfileImage(UpdateProfileImageRequest request)
    {
        var user = await GetCurrentUserInfoOrDefault() ?? throw new ValidationException(ErrorCodes.UserNotFound);

        using var image = SixLabors.ImageSharp.Image.Load(request.File.OpenReadStream());
        image.Mutate(x => x.Resize(256, 256));
        using var resultStream = new MemoryStream();
        image.Save(resultStream, new PngEncoder());

        var content = new MultipartFormDataContent();
        var streamContent = new ByteArrayContent(resultStream.ToArray());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(request.File.ContentType);
        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = request.File.Name,
            FileName = request.File.FileName,
        };
        content.Add(streamContent);
        content.Headers.Add("X-Upsert", "true");
        using var client = clientFactory.CreateClient("Supabase");
        var response =
            await client.PostAsync(
                $"{configuration["SupabaseStorageBaseUrl"]}/PublicProfilePhoto/{user.SupabaseId}/ProfilePhoto",
                content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload image");
        }

        user.ProfilePhotoUrl =
            $"{configuration["SupabaseStorageBaseUrl"]}/public/PublicProfilePhoto/{user.SupabaseId}/ProfilePhoto";
        await accountRepository.UpdateAsync(user);
    }

    public async Task<UserDTO> GetUser()
    {
        var user = await accountRepository.GetFullUser(httpContextAccessor.HttpContext!.User.GetSupabaseId()!);
        foreach (var education in user.UniversityEducations)
        {
            education.UniversityName ??= education.University?.Name;
        }

        return mapper.Map<UserDTO>(user!);
    }

    public async Task FollowCompany(int companyId, bool follow)
    {
        var supabaseId = httpContextAccessor.HttpContext!.User.GetSupabaseId()!;
        var user = await accountRepository.GetQueryable().AsTracking().Include(x => x.FollowedCompanies)
            .FirstAsync(x => x.SupabaseId == supabaseId);

        if (follow)
        {
            if (user.FollowedCompanies.Any(x => x.CompanyId == companyId))
            {
                return;
            }
            user.FollowedCompanies.Add(new UserCompanyFollow()
            {
                CompanyId = companyId,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            });
        
            await accountRepository.UpdateAsync(user);    
        }
        else
        {
            var companyFollow = user.FollowedCompanies.FirstOrDefault(x => x.CompanyId == companyId);
            if (companyFollow is not null)
            {
                user.FollowedCompanies.Remove(companyFollow);
                await accountRepository.UpdateAsync(user);
            }
        }
    }
    
    public async Task FollowPosting(int postingId, bool follow)
    {
        var supabaseId = httpContextAccessor.HttpContext!.User.GetSupabaseId()!;
        var user = await accountRepository.GetQueryable().Include(x => x.FollowedPostings)
            .FirstAsync(x => x.SupabaseId == supabaseId);

        if (follow)
        {
            if (user.FollowedPostings.Any(x => x.PostingId == postingId))
            {
                return;
            }
            user.FollowedPostings.Add(new UserPostingFollow()
            {
                PostingId = postingId,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
            });
        
            await accountRepository.UpdateAsync(user); 
        }
        else
        {
            var postingFollow = user.FollowedPostings.FirstOrDefault(x => x.PostingId == postingId);
            if (postingFollow is not null)
            {
                user.FollowedPostings.Remove(postingFollow);
                await accountRepository.UpdateAsync(user);
            }
        }
    }
}