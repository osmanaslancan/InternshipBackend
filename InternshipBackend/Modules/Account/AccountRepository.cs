using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules;

public interface IAccountRepository
{
    Task CreateAsync(User userInfo);
    Task<User?> GetBySupabaseIdAsync(Guid id);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsBySupabaseId(Guid supabaseId);
    Task UpdateAsync(User userInfo);
}

public class AccountRepository(InternshipDbContext context) : IRepository, IAccountRepository
{
    public async Task CreateAsync(User userInfo)
    {
        await context.UserInfos.AddAsync(userInfo);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User userInfo)
    {
        context.UserInfos.Update(userInfo);
        await context.SaveChangesAsync();
    }

    public Task<User?> GetBySupabaseIdAsync(Guid id)
    {
        return context.UserInfos.FirstOrDefaultAsync(x => x.SupabaseId == id);
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
