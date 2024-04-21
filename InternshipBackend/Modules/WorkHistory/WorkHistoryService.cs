using InternshipBackend.Core.Services;

namespace InternshipBackend.Modules.WorkHistory;

public interface IWorkHistoryService : IGenericEntityService<WorkHistoryModifyDto, Data.Models.WorkHistory>
{
}

public class WorkHistoryService(IServiceProvider serviceProvider)
    : GenericEntityService<WorkHistoryModifyDto, Data.Models.WorkHistory>(serviceProvider), IWorkHistoryService
{
}