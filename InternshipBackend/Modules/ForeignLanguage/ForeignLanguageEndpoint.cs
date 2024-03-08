using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("ForeignLanguage/[action]")]
public class ForeignLanguageEndpoint(IForeignLanguageService foreignLanguageService) 
    : BaseEndpoint
{
    [HttpPost]
    public async Task<ServiceResponse> CreateAsync([FromBody] ForeignLanguageDTO foreignLanguageDTO)
    {
        await foreignLanguageService.CreateAsync(foreignLanguageDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> UpdateAsync([FromBody] ForeignLanguageDTO foreignLanguageDTO)
    {
        await foreignLanguageService.UpdateAsync(foreignLanguageDTO);
        return new EmptyResponse();
    }

    [HttpPost]
    public async Task<ServiceResponse> DeleteAsync([FromBody] DeleteRequest deleteRequest)
    {
        await foreignLanguageService.DeleteAsync(deleteRequest);
        return new EmptyResponse();
    }
}
