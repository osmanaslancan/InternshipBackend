using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("UserProject/[action]")]
public class UserProjectEndpoint(IUserProjectService userProjectService) 
    : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> CreateAsync(UserProjectDTO userProjectDTO)
    {
        await userProjectService.CreateAsync(userProjectDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync(UserProjectDTO userProjectDTO)
    {
        await userProjectService.UpdateAsync(userProjectDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> DeleteAsync(DeleteRequest deleteRequest)
    {
        await userProjectService.DeleteAsync(deleteRequest);
        return new EmptyResponse();
    }
}
