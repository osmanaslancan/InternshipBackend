using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.Internship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.CompanyManagement;

[Authorize(PermissionKeys.CompanyOwner)]
[Route("Company")]
public class CompanyEndpoint(ICompanyService companyService, IInternshipPostingService internshipPostingService)
    : BaseEndpoint
{
    [HttpGet("Get")]
    public async Task<ServiceResponse<CompanyDto?>> GetCompany()
    {
        var company = await companyService.GetCurrentUsersCompany();
        return new ServiceResponse<CompanyDto?>()
        {
            Data = company
        };
    }

    [HttpPost("Update")]
    public async Task<ServiceResponse> UpdateAsync([FromBody] CompanyModifyDto dto)
    {
        await companyService.Upsert(dto);
        return new EmptyResponse();
    }

    [HttpGet("InternshipPosting/List")]
    [AllowAnonymous]
    public async Task<ServiceResponse<PagedListDto<InternshipPostingListDto>>> ListInternshipPostingAsync(
        [FromQuery] int? companyId, 
        [FromQuery] int from,
        [FromQuery] int? take = 10,
        [FromQuery] InternshipPostingSort sort = InternshipPostingSort.CreatedAt)
    {
        var result = await internshipPostingService.ListAsync(companyId, from, take, sort);
        return new()
        {
            Data = result
        };
    }

    [HttpPost("InternshipPosting/Create")]
    public async Task<ServiceResponse> CreateInternshipPostingAsync([FromBody] InternshipPostingModifyDto dto)
    {
        await internshipPostingService.CreateAsync(dto);
        return new EmptyResponse();
    }

    [HttpPost("InternshipPosting/Update/{id:int}")]
    public async Task<ServiceResponse> UpdateInternshipPostingAsync([FromRoute] int id,
        [FromBody] InternshipPostingModifyDto dto)
    {
        await internshipPostingService.UpdateAsync(id, dto);
        return new EmptyResponse();
    }

    [HttpPost("InternshipPosting/End/{id:int}")]
    public async Task<ServiceResponse> EndInternshipPostingAsync([FromRoute] int id)
    {
        await internshipPostingService.EndPostingAsync(id);
        return new EmptyResponse();
    }

    [AllowAnonymous]
    [HttpGet("InternshipPosting/Get/{id:int}")]
    public async Task<ServiceResponse<InternshipPostingDto>> GetInternshipPostingAsync([FromRoute] int id)
    {
        var data = await internshipPostingService.GetPostingAsync(id);
        return new ServiceResponse<InternshipPostingDto>()
        {
            Data = data
        };
    }

    [HttpGet("InternshipPosting/GetApplications/{id:int}")]
    public async Task<ServiceResponse<List<InternshipApplicationCompanyDto>>> GetInternshipPostingApplicationsAsync(
        [FromRoute] int id)
    {
        var data = await internshipPostingService.GetApplications(id);
        return new ServiceResponse<List<InternshipApplicationCompanyDto>>()
        {
            Data = data
        };
    }
    
    [HttpGet("InternshipPosting/GetApplicationDetail/{id:int}")]
    public async Task<ServiceResponse<ApplicationDetailDto>> ApplicationDetailAsync(
        [FromRoute] int id)
    {
        var data = await internshipPostingService.GetApplicationDetail(id);
        return new ServiceResponse<ApplicationDetailDto>()
        {
            Data = data
        };
    }
}