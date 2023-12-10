using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules;

public interface IUserInfoRepository
{
    Task CreateAsync(UserInfo userInfo);
    Task<UserInfo?> GetByUserIdAsync(Guid id);
}

public class UserInfoRepository(InternshipDbContext context) : IRepository, IUserInfoRepository
{
    public async Task CreateAsync(UserInfo userInfo)
    {
        await context.UserInfos.AddAsync(userInfo);
        await context.SaveChangesAsync();
    }

    public Task<UserInfo?> GetByUserIdAsync(Guid id)
    {
        return context.UserInfos.Include(x => x.University).FirstOrDefaultAsync(x => x.SupabaseId == id);
    }
}
