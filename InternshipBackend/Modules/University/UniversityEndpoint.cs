using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules;

[Route("University/[action]")]
public class UniversityEndpoint(IUniversityService universityService) : BaseEndpoint
{
    [HttpGet]
    public async Task<ServiceResponse<List<University>>> ListAsync()
    {
        return ServiceResponse.Success(await universityService.ListAsync());
    }
}