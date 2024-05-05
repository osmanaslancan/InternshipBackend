using InternshipBackend.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.UserDetail;

public interface IUserDetailService : IGenericEntityService<UserDetailDto, Data.Models.UserDetail>
{
    Task Upsert(UserDetailDto data);
}

public class UserDetailService(IServiceProvider serviceProvider)
    : GenericEntityService<UserDetailDto, Data.Models.UserDetail>(serviceProvider), IUserDetailService
{
    public Task Upsert(UserDetailDto data)
    {
        var user = userRetriver.GetCurrentUser(x => x.Include(y => y.Detail)) ?? throw new Exception("User not found");

        if (user.Detail is null)
        {
            return CreateAsync(data);
        }

        return base.UpdateAsync(user.Id, data);
    }
}