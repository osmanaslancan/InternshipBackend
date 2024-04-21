using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.University;

[Route("University/[action]")]
public class UniversityEndpoint(IUniversityService universityService) : BaseEndpoint
{
    [HttpGet, AllowAnonymous]
    public async Task<ServiceResponse<List<Data.Models.University>>> ListAsync()
    {
        return ServiceResponse.Success(await universityService.ListAsync());
    }
}