using InternshipBackend.Core;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules;

[Route("University/[action]")]
public class UniversityEndpoint(IUniversityService universityService) : ServiceEndpoint
{
    [HttpGet]
    public async Task<ServiceResponse<List<University>>> ListAsync()
    {
        return ServiceResponse.Success(await universityService.ListAsync());
    }
}