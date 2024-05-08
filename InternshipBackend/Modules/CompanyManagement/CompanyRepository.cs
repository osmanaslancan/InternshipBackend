using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.CompanyManagement;

public interface ICompanyRepository
{
    Task<int?> GetIdByUserIdOrDefaultAsync(int userId);
}

public class CompanyRepository(InternshipDbContext dbContext) 
    : GenericRepository<Company>(dbContext), 
    IGenericRepository<Company>, ICompanyRepository
{
    public async Task<int?> GetIdByUserIdOrDefaultAsync(int userId)
    {
        var result = await dbContext.Companies
            .Where(c => c.AdminUserId == userId)
            .FirstOrDefaultAsync();

        return result?.Id;
    }
}