using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.WorkHistory;

[Route("WorkHistory/[action]")]
public class WorkHistoryEndpoint(IWorkHistoryService workHistoryService)
        : CrudEndpoint<WorkHistoryModifyDto, Data.Models.WorkHistory>(workHistoryService)
{
}
