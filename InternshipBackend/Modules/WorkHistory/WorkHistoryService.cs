using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IWorkHistoryService : IGenericEntityService<WorkHistoryDto, WorkHistory>
{
}

public class WorkHistoryService(IServiceProvider serviceProvider)
    : GenericEntityService<WorkHistoryDto, WorkHistory>(serviceProvider), IWorkHistoryService
{
}