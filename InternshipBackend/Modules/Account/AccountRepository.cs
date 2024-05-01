using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.Account;

public interface IAccountRepository : IGenericRepository<User>
{
    Task<User?> GetBySupabaseIdAsync(Guid id);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsBySupabaseId(Guid supabaseId);
    Task<User> GetFullUser(Guid supabaseId);
    Task<bool> HasPermissionWithSupabaseId(Guid supabaseId, string permission);
    Task<bool> HasTypeWithSupabaseId(Guid supabaseId, AccountType accountType);
}

public class AccountRepository(InternshipDbContext context) : GenericRepository<User>(context), IAccountRepository
{
    public Task<User?> GetBySupabaseIdAsync(Guid id)
    {
        return DbContext.Users.FirstOrDefaultAsync(x => x.SupabaseId == id);
    }

    public Task<bool> HasPermissionWithSupabaseId(Guid supabaseId, string permission)
    {
        return DbContext.Users.AnyAsync(user =>
            user.SupabaseId == supabaseId && user.UserPermissions.Any(userPermission => userPermission.Permission == permission));
    }
    
    public Task<bool> HasTypeWithSupabaseId(Guid supabaseId, AccountType accountType)
    {
        return DbContext.Users.AnyAsync(user =>
            user.SupabaseId == supabaseId && user.AccountType == accountType);
    }

    public Task<bool> ExistsByEmail(string email)
    {
        return DbContext.Users.AnyAsync(x => x.Email == email);
    }

    public Task<bool> ExistsBySupabaseId(Guid supabaseId)
    {
        return DbContext.Users.AnyAsync(x => x.SupabaseId == supabaseId);
    }

    public async Task<User> GetFullUser(Guid supabaseId)
    {
        var result = await DbContext.Users
            .Include(x => x.ForeignLanguages)
            .Include(x => x.UniversityEducations)
            .Include(x => x.Works)
            .Include(x => x.UserProjects)
            .Include(x => x.Detail)
            .Include("UniversityEducations.University")
            .FirstAsync(x => x.SupabaseId == supabaseId);

        return result;
    }
}
