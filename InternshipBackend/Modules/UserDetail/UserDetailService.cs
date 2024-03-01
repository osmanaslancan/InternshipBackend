using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserDetailService : IGenericService<UserDetailDTO, UserDetailDTO, UserDetail, UserDetail>
{
}

public class UserDetailService(IServiceProvider serviceProvider, IUserRetriverService userRetriver)
    : GenericService<UserDetailDTO, UserDetailDTO, UserDetail, UserDetail>(serviceProvider), IUserDetailService
{
    public override Task UpdateAsync(UserDetailDTO data)
    {
        var user = userRetriver.GetCurrentUser(x => x.Include(y => y.Detail)) ?? throw new Exception("User not found");

        if (user.Detail is null)
        {
            return CreateAsync(data);
        }

        return base.UpdateAsync(data);
    }
}