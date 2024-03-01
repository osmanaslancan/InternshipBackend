using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IWorkHistoryService : IGenericService<WorkHistoryDTO, WorkHistoryDTO, DeleteRequest, WorkHistory>
{
}

public class WorkHistoryService(IServiceProvider serviceProvider, IUserRetriverService userRetriver)
    : GenericService<WorkHistoryDTO, WorkHistoryDTO, DeleteRequest, WorkHistory>(serviceProvider), IWorkHistoryService
{
    protected override Task BeforeCreate(WorkHistory data)
    {
        var user = userRetriver.GetCurrentUser();
        data.UserId = user.UserId;

        return base.BeforeCreate(data);
    }

    protected override async Task BeforeUpdate(WorkHistory data)
    {
        var user = userRetriver.GetCurrentUser();

        var old = (await _repository.GetByIdOrDefaultAsync(data.WorkHistoryId)) ?? throw new Exception("Record not found");

        if (old.UserId != data.UserId || old.UserId != user.UserId)
        {
            throw new Exception("You can't update other user's data");
        }
    }

    protected override Task BeforeDelete(WorkHistory data)
    {
        var user = userRetriver.GetCurrentUser();

        if (data.UserId != user.UserId)
        {
            throw new Exception("You can't delete other user's data");
        }

        return base.BeforeDelete(data);
    }

    protected override WorkHistory MapDelete(DeleteRequest data)
    {
        var result = Activator.CreateInstance<WorkHistory>();
        result.WorkHistoryId = (int)data.Id;

        return result;
    }
}