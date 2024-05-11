using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Data.Models.ValueObjects;
using InternshipBackend.Modules.CompanyManagement;

namespace InternshipBackend.Modules.Internship;


public interface IInternshipPostingService : IGenericEntityService<InternshipPostingModifyDto, InternshipPosting>
{
    public Task<InternshipPosting> EndPostingAsync(int id);
    public Task ApplyToPosting(InternshipApplicationDto dto);
    Task<PagedListDto<InternshipPostingListDto>> ListAsync(int? companyId, int from);
    Task CommentOnPosting(InternshipCommentDto dto);
}

public class InternshipPostingService(IServiceProvider serviceProvider, ICompanyService companyService, IInternshipPostingRepository repository)
    : GenericEntityService<InternshipPostingModifyDto, InternshipPosting>(serviceProvider), IInternshipPostingService
{
    protected override async Task BeforeCreate(InternshipPosting data)
    {
        await base.BeforeCreate(data);
        
        data.CompanyId = await companyService.GetCurrentUserCompanyId();
        data.CreatedAt = DateTime.UtcNow;
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
        
        if (posting.Comments.Count(x => x.UserId == user.Id) > 2)
        {
            throw new ValidationException("You can only comment 2 times on a posting.");
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

    public async Task<PagedListDto<InternshipPostingListDto>> ListAsync(int? companyId, int from)
    {
        var postings = await repository.ListCompanyPostingsAsync(companyId, from);
        var total = await repository.CountCompanyPostingsAsync(companyId);
        
        var result = mapper.Map<List<InternshipPostingListDto>>(postings);
        
        return new()
        {
            Items = result,
            From = from,
            Count = result.Count,
            Total = total
        };
    }
}