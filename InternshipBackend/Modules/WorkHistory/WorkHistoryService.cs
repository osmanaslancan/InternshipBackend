using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IWorkHistoryService : IGenericService<WorkHistoryDTO, WorkHistoryDTO, DeleteRequest, WorkHistory>
{
}

public class WorkHistoryService(IServiceProvider serviceProvider)
    : GenericService<WorkHistoryDTO, WorkHistoryDTO, DeleteRequest, WorkHistory>(serviceProvider), IWorkHistoryService
{
}