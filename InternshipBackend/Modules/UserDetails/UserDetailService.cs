using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Enums;
using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.UserDetails;

public interface IUserDetailService : IGenericEntityService<UserDetailDto, Data.Models.UserDetail>
{
    Task Upsert(UserDetailDto data);
    Task AddCvToCurrentUser(string filename, string cvUrl);
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

    public async Task AddCvToCurrentUser(string filename, string cvUrl)
    {
        var user = UserRetriever.GetCurrentUser(x => x.Include(y => y.Detail)) ?? throw new Exception("User not found");
        user.Detail ??= new UserDetail()
        {
            User = user,
        };
        
        user.Detail.Cvs.Add(new UserCv()
        {
            FileUrl = cvUrl,
            FileName = filename,
        });
        
        await Repository.UpdateAsync(user.Detail);
    }
}