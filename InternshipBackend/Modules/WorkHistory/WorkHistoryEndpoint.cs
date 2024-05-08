using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.WorkHistory;

[Route("WorkHistory")]
[Authorize(PermissionKeys.Intern)]
public class WorkHistoryEndpoint(IWorkHistoryService workHistoryService)
        : CrudEndpoint<WorkHistoryModifyDto, Data.Models.WorkHistory>(workHistoryService)
{
}
