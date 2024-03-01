using InternshipBackend.Data;
using System.Security.Claims;

namespace InternshipBackend.Core;

public interface IUserRetriverService
{
    User GetCurrentUser(Func<IQueryable<User>, IQueryable<User>>? edit = null);
    User? GetCurrentUserOrDefault(Func<IQueryable<User>, IQueryable<User>>? edit = null);
}
public class UserRetriever(InternshipDbContext dbContext, IHttpContextAccessor httpContextAccessor) : IUserRetriverService, IService
{
    public User GetCurrentUser(Func<IQueryable<User>, IQueryable<User>>? edit = null)
    {
        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        IQueryable<User> queryable = dbContext.Users;
        if (edit is not null)
            queryable = edit.Invoke(queryable);

        return queryable.FirstOrDefault(x => x.SupabaseId == supabaseId) ?? throw new Exception("User not found");
    }

    public User? GetCurrentUserOrDefault(Func<IQueryable<User>, IQueryable<User>>? edit = null)
    {
        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        IQueryable<User> queryable = dbContext.Users;
        if (edit is not null)
            queryable = edit.Invoke(queryable);

        return queryable.FirstOrDefault(x => x.SupabaseId == supabaseId);
    }
}
