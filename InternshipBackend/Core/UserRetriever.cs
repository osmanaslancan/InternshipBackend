using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Core;

public interface IUserRetriverService
{
    User GetCurrentUser(Func<IQueryable<User>, IQueryable<User>>? edit = null);
    User? GetCurrentUserOrDefault(Func<IQueryable<User>, IQueryable<User>>? edit = null);
}
public class UserRetriever(InternshipDbContext dbContext, IHttpContextAccessor httpContextAccessor) : IUserRetriverService, IScopedService
{
    public User GetCurrentUser(Func<IQueryable<User>, IQueryable<User>>? edit = null)
    {
        return GetCurrentUserOrDefault(edit) ?? throw new Exception("User not found");
    }

    public User? GetCurrentUserOrDefault(Func<IQueryable<User>, IQueryable<User>>? edit = null)
    {
        var supabaseId = Guid.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        IQueryable<User> queryable = dbContext.Users.AsNoTracking();
        if (edit is not null)
            queryable = edit.Invoke(queryable);

        return queryable.FirstOrDefault(x => x.SupabaseId == supabaseId);
    }
}
