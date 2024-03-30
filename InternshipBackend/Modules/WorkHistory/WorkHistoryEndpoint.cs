using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("WorkHistory/[action]")]
public class WorkHistoryEndpoint(IWorkHistoryService workHistoryService)
        : CrudEndpoint<WorkHistoryDto, WorkHistory>(workHistoryService)
{
}
