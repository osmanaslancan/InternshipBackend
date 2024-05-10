using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.CompanyManagement;

public interface ICompanyRepository
{
    Task<Company?> GetByUserIdOrDefaultAsync(int userId);
}

public class CompanyRepository(InternshipDbContext dbContext) 
    : GenericRepository<Company>(dbContext), 
    IGenericRepository<Company>, ICompanyRepository
{
    public async Task<Company?> GetByUserIdOrDefaultAsync(int userId)
    {
        var result = await DbContext.Companies.AsNoTracking()
            .Where(c => c.AdminUserId == userId)
            .FirstOrDefaultAsync();

        return result;
    }
}