using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("WorkHistory/[action]")]
public class WorkHistoryEndpoint(IWorkHistoryService workHistoryService) 
    : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> CreateAsync(WorkHistoryDTO workHistoryDTO)
    {
        await workHistoryService.CreateAsync(workHistoryDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync(WorkHistoryDTO workHistoryDTO)
    {
        await workHistoryService.UpdateAsync(workHistoryDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> DeleteAsync(DeleteRequest deleteRequest)
    {
        await workHistoryService.DeleteAsync(deleteRequest);
        return new EmptyResponse();
    }
}
