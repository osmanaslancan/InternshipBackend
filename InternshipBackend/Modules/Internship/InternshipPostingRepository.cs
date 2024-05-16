// using InternshipBackend.Core;

using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Internship;

public interface IInternshipPostingRepository : IGenericRepository<InternshipPosting>
{
    Task<List<InternshipPosting>> ListCompanyPostingsAsync(int? companyId, int from, int? take,
        InternshipPostingSort sort);

    Task<int> CountCompanyPostingsAsync(int? companyId);
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


    public async Task<List<InternshipPosting>> ListCompanyPostingsAsync(int? companyId, int from, int? take,
        InternshipPostingSort sort)
    {
        var query = DbContext.InternshipPostings
            .WhereIf(companyId != null, x => x.CompanyId == companyId);

        if (sort == InternshipPostingSort.Popularity)
        {
            query = query.OrderByDescending(x => x.Applications.Count);
        }
        else
        {
            query = query.OrderByDescending(x => x.CreatedAt);
        }

        return await query.Skip(from)
            .Take(Math.Min(100, take ?? 10)).ToListAsync();
    }

    public async Task<int> CountCompanyPostingsAsync(int? companyId)
    {
        return await DbContext.InternshipPostings
            .WhereIf(companyId != null, x => x.CompanyId == companyId)
            .CountAsync();
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