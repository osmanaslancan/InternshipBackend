using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules;

public interface IAccountRepository
{
    Task CreateAsync(UserInfo userInfo);
    Task<UserInfo?> GetBySupabaseIdAsync(Guid id);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsBySupabaseId(Guid supabaseId);
}

public class AccountRepository(InternshipDbContext context) : IRepository, IAccountRepository
{
    public async Task CreateAsync(UserInfo userInfo)
    {
        await context.UserInfos.AddAsync(userInfo);
        await context.SaveChangesAsync();
    }

    public Task<UserInfo?> GetBySupabaseIdAsync(Guid id)
    {
        return context.UserInfos.Include(x => x.University).FirstOrDefaultAsync(x => x.SupabaseId == id);
    }

    public Task<bool> ExistsByEmail(string email)
    {
        return context.UserInfos.AnyAsync(x => x.Email == email);
    }

    public Task<bool> ExistsBySupabaseId(Guid supabaseId)
    {
        return context.UserInfos.AnyAsync(x => x.SupabaseId == supabaseId);
    }
}
