using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("UniversityEducation/[action]")]
public class UniversityEducationEndpoint(IUniversityEducationService userDetailService) 
    : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> CreateAsync(UniversityEducationDTO UniversityEducationDTO)
    {
        await userDetailService.CreateAsync(UniversityEducationDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync(UniversityEducationDTO UniversityEducationDTO)
    {
        await userDetailService.UpdateAsync(UniversityEducationDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> DeleteAsync(DeleteRequest deleteRequest)
    {
        await userDetailService.DeleteAsync(deleteRequest);
        return new EmptyResponse();
    }
}
