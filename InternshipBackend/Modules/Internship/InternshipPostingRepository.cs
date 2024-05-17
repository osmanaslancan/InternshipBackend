// using InternshipBackend.Core;

using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Internship;

public interface IInternshipPostingRepository : IGenericRepository<InternshipPosting>
{
    Task<List<InternshipPosting>> ListCompanyPostingsAsync(InternshipPostingListRequestDto request);

    Task<int> CountCompanyPostingsAsync(InternshipPostingListRequestDto request);
    Task<InternshipPosting?> GetDetailedByIdOrDefaultAsync(int id, bool changeTracking = true);
    Task<InternshipApplication?> GetInternshipApplication(int id);
    IQueryable<InternshipPosting> GetQueryable();
}

public class InternshipPostingRepository(InternshipDbContext dbContext)
    : GenericRepository<InternshipPosting>(dbContext),
        IGenericRepository<InternshipPosting>, IInternshipPostingRepository
{
    public IQueryable<InternshipPosting> GetQueryable()
    {
        return DbContext.InternshipPostings.AsNoTracking();
    }

    private IQueryable<InternshipPosting> GetQuery(InternshipPostingListRequestDto request)
    {
        return DbContext.InternshipPostings
            .WhereIf(request.CompanyId != null, x => x.CompanyId == request.CompanyId)
            .WhereIf(!string.IsNullOrWhiteSpace(request.MatchQuery),
                x => x.SearchVector.Matches(EF.Functions.PlainToTsQuery("turkish", request.MatchQuery!)) ||
                     EF.Functions.TrigramsSimilarity(x.Title, request.MatchQuery!) > 0.3)
            .WhereIf(request.WorkType != null, x => x.WorkType == request.WorkType)
            .WhereIf(request.EmploymentType != null, x => x.EmploymentType == request.EmploymentType)
            .WhereIf(request.Salary != null, x => x.HasSalary == request.Salary);
    }

    public async Task<List<InternshipPosting>> ListCompanyPostingsAsync(InternshipPostingListRequestDto request)
    {
        var query = GetQuery(request);
        
        if (request.Sort == InternshipPostingSort.Popularity)
        {
            query = query.OrderByDescending(x => x.Applications.Count);
        }
        else
        {
            query = query.OrderByDescending(x => x.CreatedAt);
        }

        return await query.Skip(request.From)
            .Take(Math.Min(100, request.Take ?? 10)).ToListAsync();
    }

    public async Task<int> CountCompanyPostingsAsync(InternshipPostingListRequestDto request)
    {
        return await GetQuery(request).CountAsync();
    }

    public async Task<InternshipPosting?> GetDetailedByIdOrDefaultAsync(int id, bool changeTracking = true)
    {
        var queryable = DbContext.InternshipPostings.AsQueryable();

        if (!changeTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        return await queryable
            .Include(x => x.Applications)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public Task<InternshipApplication?> GetInternshipApplication(int id)
    {
        var queryable = DbContext.InternshipPostings.AsQueryable()
            .Where(x => x.Applications.Any(x => x.Id == id))
            .Select(x => x.Applications.First(x => x.Id == id));

        var result = queryable.FirstOrDefaultAsync();

        return result;
    }
}