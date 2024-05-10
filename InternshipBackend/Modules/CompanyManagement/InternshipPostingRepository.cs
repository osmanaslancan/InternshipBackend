// using InternshipBackend.Core;

using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.CompanyManagement;

public interface IInternshipPostingRepository : IGenericRepository<InternshipPosting>
{
    Task<List<InternshipPosting>> ListCompanyPostingsAsync(int? companyId, int from);
    Task<int> CountCompanyPostingsAsync(int? companyId);
}

public class InternshipPostingRepository(InternshipDbContext dbContext) 
    : GenericRepository<InternshipPosting>(dbContext), 
    IGenericRepository<InternshipPosting>, IInternshipPostingRepository
{
    public async Task<List<InternshipPosting>> ListCompanyPostingsAsync(int? companyId, int from)
    {
        return await DbContext.InternshipPostings
            .WhereIf(companyId != null, x => x.CompanyId == companyId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(from)
            .Take(100)
            .ToListAsync();
    }
    
    public async Task<int> CountCompanyPostingsAsync(int? companyId)
    {
        return await DbContext.InternshipPostings
            .WhereIf(companyId != null, x => x.CompanyId == companyId)
            .CountAsync();
    }
}