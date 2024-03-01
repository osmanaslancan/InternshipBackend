using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserProjectService : IGenericService<UserProjectDTO, UserProjectDTO, DeleteRequest, UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider, IUserRetriverService userRetriver)
    : GenericService<UserProjectDTO, UserProjectDTO, DeleteRequest, UserProject>(serviceProvider), IUserProjectService
{
    protected override Task BeforeCreate(UserProject data)
    {
        var user = userRetriver.GetCurrentUser();
        data.UserId = user.UserId;

        return base.BeforeCreate(data);
    }

    protected override async Task BeforeUpdate(UserProject data)
    {
        var user = userRetriver.GetCurrentUser();

        var old = (await _repository.GetByIdOrDefaultAsync(data.UserProjectId)) ?? throw new Exception("Record not found");

        if (old.UserId != data.UserId || old.UserId != user.UserId)
        {
            throw new Exception("You can't update other user's data");
        }
    }

    protected override Task BeforeDelete(UserProject data)
    {
        var user = userRetriver.GetCurrentUser();

        if (data.UserId != user.UserId)
        {
            throw new Exception("You can't delete other user's data");
        }

        return base.BeforeDelete(data);
    }

    protected override UserProject MapDelete(DeleteRequest data)
    {
        var result = Activator.CreateInstance<UserProject>();
        result.UserProjectId = (int)data.Id;

        return result;
    }
}