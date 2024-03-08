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
    public async Task<ServiceResponse> CreateAsync([FromBody] UniversityEducationDTO universityEducationDTO)
    {
        await userDetailService.CreateAsync(universityEducationDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync([FromBody] UniversityEducationDTO universityEducationDTO)
    {
        await userDetailService.UpdateAsync(universityEducationDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> DeleteAsync([FromBody] DeleteRequest deleteRequest)
    {
        await userDetailService.DeleteAsync(deleteRequest);
        return new EmptyResponse();
    }
}
