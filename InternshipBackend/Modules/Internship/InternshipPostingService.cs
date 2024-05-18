using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Data.Models.ValueObjects;
using InternshipBackend.Modules.Account;
using InternshipBackend.Modules.App;
using InternshipBackend.Modules.CompanyManagement;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Internship;

public interface IInternshipPostingService : IGenericEntityService<InternshipPostingModifyDto, InternshipPosting>
{
    public Task<InternshipPosting> EndPostingAsync(int id);
    public Task ApplyToPosting(InternshipApplicationDto dto);

    Task<PagedListDto<InternshipPostingListDto>> ListAsync(InternshipPostingListRequestDto listRequest);

    Task CommentOnPosting(InternshipCommentDto dto);
    Task<InternshipPostingDto> GetPostingAsync(int id);
    Task<List<InternshipApplicationCompanyDto>> GetApplications(int id);
    Task<ApplicationDetailDto> GetApplicationDetail(int id);
}

public class InternshipPostingService(
    IServiceProvider serviceProvider,
    ICompanyService companyService,
    ICompanyRepository companyRepository,
    IInternshipPostingRepository repository,
    IUploadCvService uploadCvService,
    IHttpContextAccessor httpContextAccessor,
    NotificationService notificationService,
    IAccountRepository accountRepository)
    : GenericEntityService<InternshipPostingModifyDto, InternshipPosting>(serviceProvider), IInternshipPostingService
{
    protected override async Task BeforeCreate(InternshipPosting data)
    {
        await base.BeforeCreate(data);

        data.CompanyId = await companyService.GetCurrentUserCompanyId();
        data.CreatedAt = DateTime.UtcNow;
    }

    public override async Task<InternshipPosting> CreateAsync(InternshipPostingModifyDto data)
    {
        var result = await base.CreateAsync(data);
        var company = await companyRepository.GetByIdOrDefaultAsync(result.CompanyId);
        await notificationService.SendNotificationToCompanyFollowers(result.CompanyId,
            $"Yeni Staj İlanı - {company!.Name}", result.Title[..Math.Min(result.Title.Length, 250)]);

        return result;
    }

    protected override async Task BeforeUpdate(InternshipPosting data, InternshipPosting old)
    {
        await base.BeforeUpdate(data, old);

        var userCompanyId = await companyService.GetCurrentUserCompanyId();
        if (old.CompanyId != userCompanyId)
        {
            throw new Exception("You can't update other company's data");
        }

        data.CompanyId = old.CompanyId;
        data.CreatedAt = old.CreatedAt;
        data.UpdatedAt = DateTime.UtcNow;
    }


    public override Task<InternshipPosting> DeleteAsync(int id)
    {
        throw new InvalidOperationException("Deleting internship postings is not allowed.");
    }

    public async Task<InternshipPosting> EndPostingAsync(int id)
    {
        var posting = await repository.GetByIdOrDefaultAsync(id);

        if (posting == null)
        {
            throw new Exception("Posting not found");
        }

        var userCompanyId = await companyService.GetCurrentUserCompanyId();

        if (posting.CompanyId != userCompanyId)
        {
            throw new Exception("You can't end other company's posting");
        }

        posting.DeadLine = DateTime.UtcNow;
        posting.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(posting);
        await _repository.SaveChangesAsync();

        return posting;
    }

    public async Task ApplyToPosting(InternshipApplicationDto dto)
    {
        await serviceProvider.GetRequiredService<IValidator<InternshipApplicationDto>>().ValidateAndThrowAsync(dto);

        var user = UserRetriever.GetCurrentUser();
        if (user.AccountType != AccountType.Intern)
        {
            throw new ValidationException("Only interns can apply to postings");
        }

        var posting = await repository.GetDetailedByIdOrDefaultAsync(dto.InternshipPostingId);

        if (posting == null)
        {
            throw new Exception("Posting not found");
        }

        if (posting.DeadLine < DateTime.UtcNow)
        {
            throw new ValidationException("Posting is closed");
        }

        if (posting.Applications.Any(x => x.UserId == user.Id))
        {
            throw new ValidationException("You have already applied to this posting");
        }

        var application = new InternshipApplication()
        {
            InternshipPostingId = dto.InternshipPostingId,
            UserId = user.Id,
            Message = dto.Message,
            CvUrl = dto.CvUrl,
            CreatedAt = DateTime.UtcNow,
        };

        posting.Applications.Add(application);

        await _repository.UpdateAsync(posting);
    }

    public async Task CommentOnPosting(InternshipCommentDto dto)
    {
        await serviceProvider.GetRequiredService<IValidator<InternshipCommentDto>>().ValidateAndThrowAsync(dto);

        var user = UserRetriever.GetCurrentUser();
        if (user.AccountType != AccountType.Intern)
        {
            throw new ValidationException("Only interns can apply to postings");
        }

        var posting = await repository.GetDetailedByIdOrDefaultAsync(dto.InternshipPostingId);

        if (posting == null)
        {
            throw new Exception("Posting not found");
        }

        if (posting.DeadLine > DateTime.UtcNow)
        {
            throw new ValidationException("Cannot comment on open postings.");
        }

        if (posting.Comments.Any(x => x.UserId == user.Id))
        {
            throw new ValidationException(ErrorCodes.ExceededComments);
        }

        var comment = new InternshipPostingComment()
        {
            Comment = dto.Comment!,
            Points = dto.Points,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow
        };

        posting.Comments.Add(comment);

        await _repository.UpdateAsync(posting);
    }

    public async Task<InternshipPostingDto> GetPostingAsync(int id)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);

        var posting = await repository.GetDetailedByIdOrDefaultAsync(id);
        if (posting == null)
        {
            throw new Exception("Posting not found");
        }


        var averageRating = (await companyService.GetAverageRatings(posting.CompanyId)).First();
        var dto = mapper.Map<InternshipPosting, InternshipPostingDto>(posting);
        dto.Company = mapper.Map<InternshipPostingCompanyDto>(averageRating);

        var userIds = posting.Comments.Select(x => x.UserId).Distinct().ToList();

        var users = await accountRepository.GetQueryable().Where(x => userIds.Contains(x.Id)).ToListAsync();

        if (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var supabaseId = httpContextAccessor.HttpContext.User.GetSupabaseId();
            var companyFollows = await accountRepository.GetCompanyFollows(supabaseId);
            var postingFollows = await accountRepository.GetPostingFollows(supabaseId);

            dto.IsCurrentUserFollowing = postingFollows.Any(x => x.PostingId == dto.Id);
            dto.Company.IsCurrentUserFollowing = companyFollows.Any(x => x.CompanyId == dto.Company.CompanyId);
        }

        foreach (var comment in dto.Comments)
        {
            var user = users.First(x => x.Id == comment.UserId);
            comment.UserName = user.Name;
            comment.UserSurname = user.Surname;
            comment.PhotoUrl = user.ProfilePhotoUrl;
        }

        dto.NumberOfComments = posting.Comments.Count;
        dto.AveragePoint = posting.Comments.Count > 0 ? posting.Comments.Average(x => x.Points) : 0;
        dto.NumberOfApplications = posting.Applications.Count;

        return dto;
    }

    public async Task<List<InternshipApplicationCompanyDto>> GetApplications(int id)
    {
        var posting = await repository.GetDetailedByIdOrDefaultAsync(id);

        if (posting == null)
        {
            throw new Exception("Posting not found");
        }

        if (posting.CompanyId != await companyService.GetCurrentUserCompanyId())
        {
            throw new Exception("You can't see other company's applications");
        }

        var applications = posting.Applications.ToList();
        var userIds = applications.Select(x => x.UserId).Distinct().ToList();
        var users = (await accountRepository.GetQueryable().Where(x => userIds.Contains(x.Id)).ToListAsync())
            .ToDictionary(x => x.Id);

        var dto = mapper.Map<List<InternshipApplication>, List<InternshipApplicationCompanyDto>>(applications);

        foreach (var application in dto)
        {
            var user = users[application.UserId];
            application.Name = user.Name;
            application.Surname = user.Surname;
            application.ProfilePhotoUrl = user.ProfilePhotoUrl;

            if (application.CvUrl != null)
            {
                application.CvUrl =
                    uploadCvService.GetDownloadUrlForCurrentUser(user.SupabaseId, Guid.Parse(application.CvUrl));
            }
        }

        return dto;
    }

    public async Task<ApplicationDetailDto> GetApplicationDetail(int id)
    {
        var application = await repository.GetInternshipApplication(id);
        if (application == null)
        {
            throw new Exception("Application not found");
        }

        var posting = await repository.GetByIdOrDefaultAsync(application.InternshipPostingId);

        if (posting == null)
        {
            throw new Exception("Posting not found");
        }

        if (posting.CompanyId != await companyService.GetCurrentUserCompanyId())
        {
            throw new Exception("You can't see other company's applications");
        }

        var user = await accountRepository.GetFullUser(application.UserId);

        var applicationDetail = mapper.Map<User, ApplicationDetailDto>(user);

        applicationDetail.InternshipPostingId = application.InternshipPostingId;
        applicationDetail.Message = application.Message;
        if (application.CvUrl is not null)
        {
            applicationDetail.CvUrl =
                uploadCvService.GetDownloadUrlForCurrentUser(user.SupabaseId, Guid.Parse(application.CvUrl));
        }

        applicationDetail.Id = application.Id;

        return applicationDetail;
    }

    public async Task<PagedListDto<InternshipPostingListDto>> ListAsync(InternshipPostingListRequestDto listRequest)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
        var postings =
            await repository.ListCompanyPostingsAsync(listRequest);
        var total = await repository.CountCompanyPostingsAsync(listRequest);
        var averageRatings = await companyService.GetAverageRatings(listRequest.CompanyId);

        var result = mapper.Map<List<InternshipPosting>, List<InternshipPostingListDto>>(postings, (o) =>
        {
            o.AfterMap((src, dest) =>
            {
                foreach (var posting in src)
                {
                    var companyRating = averageRatings.First(x => x.CompanyId == posting.CompanyId);
                    var destData = dest.First(x => x.Id == posting.Id);
                    destData.Company = mapper.Map<InternshipPostingCompanyDto>(companyRating);
                }
            });
        });

        if (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
        {
            var supabaseId = httpContextAccessor.HttpContext.User.GetSupabaseId();
            var companyFollows = await accountRepository.GetCompanyFollows(supabaseId);
            var postingFollows = await accountRepository.GetPostingFollows(supabaseId);

            foreach (var item in result)
            {
                item.IsCurrentUserFollowing = postingFollows.Any(x => x.PostingId == item.Id);
                item.Company.IsCurrentUserFollowing = companyFollows.Any(x => x.CompanyId == item.Company.CompanyId);
            }
        }

        return new PagedListDto<InternshipPostingListDto>
        {
            Items = result,
            From = listRequest.From,
            Count = result.Count,
            Total = total
        };
    }
}