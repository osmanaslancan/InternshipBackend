using InternshipBackend.Core;
using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules;

public interface IAccountRepository : IGenericRepository<User>
{
    Task<User?> GetBySupabaseIdAsync(Guid id);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsBySupabaseId(Guid supabaseId);
}

public class AccountRepository(InternshipDbContext context) : GenericRepository<User>(context), IAccountRepository
{
    public Task<User?> GetBySupabaseIdAsync(Guid id)
    {
        return DbContext.Users.FirstOrDefaultAsync(x => x.SupabaseId == id);
    }

    public Task<bool> ExistsByEmail(string email)
    {
        return DbContext.Users.AnyAsync(x => x.Email == email);
    }

    public Task<bool> ExistsBySupabaseId(Guid supabaseId)
    {
        return DbContext.Users.AnyAsync(x => x.SupabaseId == supabaseId);
    }
}
