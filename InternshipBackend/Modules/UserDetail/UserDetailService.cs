﻿using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserDetailService : IGenericEntityService<UserDetailDTO, UserDetail>
{
    Task Upsert(UserDetailDTO data);
}

public class UserDetailService(IServiceProvider serviceProvider)
    : GenericEntityService<UserDetailDTO, UserDetail>(serviceProvider), IUserDetailService
{
    public Task Upsert(UserDetailDTO data)
    {
        var user = userRetriver.GetCurrentUser(x => x.Include(y => y.Detail)) ?? throw new Exception("User not found");

        if (user.Detail is null)
        {
            return CreateAsync(data);
        }

        return base.UpdateAsync(user.Id, data);
    }
}