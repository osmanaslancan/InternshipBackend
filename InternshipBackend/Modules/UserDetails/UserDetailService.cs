using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.UserDetails;

public interface IUserDetailService : IGenericEntityService<UserDetailDto, Data.Models.UserDetail>
{
    Task Upsert(UserDetailDto data);
}

public class UserDetailService(IServiceProvider serviceProvider)
    : GenericEntityService<UserDetailDto, Data.Models.UserDetail>(serviceProvider), IUserDetailService
{
    public Task Upsert(UserDetailDto data)
    {
        var user = UserRetriever.GetCurrentUser(x => x.Include(y => y.Detail)) ?? throw new Exception("User not found");

        if (user.Detail is null)
        {
            return CreateAsync(data);
        }

        return base.UpdateAsync(user.Id, data);
    }

    protected override UserDetail MapDto(UserDetailDto data)
    {
        data.DriverLicenses = data.DriverLicenses.Where(x => x != DriverLicense.None).ToList();
        return base.MapDto(data);
    }
}