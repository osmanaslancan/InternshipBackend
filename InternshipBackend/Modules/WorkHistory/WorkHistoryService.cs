using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IWorkHistoryService : IGenericService<WorkHistoryDto, WorkHistory>
{
}

public class WorkHistoryService(IServiceProvider serviceProvider)
    : GenericService<WorkHistoryDto, WorkHistory>(serviceProvider), IWorkHistoryService
{
}