using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.CompanyManagement;

public interface ICompanyRepository : IGenericRepository<Company>
{
    IQueryable<Company> GetQueryable();
    Task<Company?> GetByUserIdOrDefaultAsync(int userId);
    Task<List<RatingResult>> GetAverageRatings(int? companyId);
    Task<Company?> GetDetailedCompany(int companyId);
}

public class CompanyRepository(InternshipDbContext dbContext) 
    : GenericRepository<Company>(dbContext), 
    IGenericRepository<Company>, ICompanyRepository
{
    public IQueryable<Company> GetQueryable()
    {
        return DbContext.Companies.AsNoTracking();
    }

    public async Task<Company?> GetByUserIdOrDefaultAsync(int userId)
    {
        var result = await DbContext.Companies.AsNoTracking()
            .Where(c => c.AdminUserId == userId)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<RatingResult>> GetAverageRatings(int? companyId)
    {
        var query = from company in DbContext.Companies.AsNoTracking()
            join posting in DbContext.InternshipPostings.AsNoTracking() on company.Id equals posting.CompanyId into
                postings
            where companyId == null || company.Id == companyId
            select new
            {
                company.Name,
                company.ShortDescription,
                company.LogoUrl,
                CompanyId = company.Id,
                NumberOfVotes = postings.Sum(x => x.Comments.Count),
                SumOfVotes = postings.Sum(x => x.Comments.Sum(c => c.Points)),
            };
        
        var result = await query.ToListAsync();

        return result.Select((x) => new RatingResult()
        {
            Name = x.Name,
            ShortDescription = x.ShortDescription,
            NumberOfComments = x.NumberOfVotes,
            LogoUrl = x.LogoUrl,
            AveragePoints = x.NumberOfVotes > 0 ? (double)x.SumOfVotes / x.NumberOfVotes : 0,
            CompanyId = x.CompanyId
        }).ToList();
    }

    public Task<Company?> GetDetailedCompany(int companyId)
    {
        return DbContext.Companies.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == companyId);
    }
}