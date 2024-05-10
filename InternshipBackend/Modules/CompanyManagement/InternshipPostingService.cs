using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.CompanyManagement;


public interface IInternshipPostingService : IGenericEntityService<InternshipPostingModifyDto, InternshipPosting>
{
    public Task<InternshipPosting> EndPostingAsync(int id);
    Task<PagedListDto<InternshipPostingListDto>> ListAsync(int? companyId, int from);
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

        var posting = await _repository.GetByIdOrDefaultAsync(id);
        
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