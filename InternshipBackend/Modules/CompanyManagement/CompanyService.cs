using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account;
using Microsoft.Extensions.Localization;

namespace InternshipBackend.Modules.CompanyManagement;

public interface ICompanyService : IGenericEntityService<CompanyModifyDto, Data.Models.Company>
{
    Task<Company> Upsert(CompanyModifyDto dto);
    Task<int> GetCurrentUserCompanyId();
    Task<CompanyDto?> GetCurrentUsersCompany();
    Task<List<RatingResult>> GetAverageRatings(int? companyId);
    Task<CompanyDetailDto> GetDetailedCompany(int companyId);
}

public class CompanyService(IServiceProvider serviceProvider,
    ICompanyRepository companyRepository, 
    IAccountRepository accountRepository,
    IHttpContextAccessor httpContextAccessor,
    IUserRetrieverService userRetrieverService)
    : GenericEntityService<CompanyModifyDto, Data.Models.Company>(serviceProvider), ICompanyService
{
    protected override Task BeforeCreate(Company data)
    {
        var user = userRetrieverService.GetCurrentUser();

        data.AdminUserId = user.Id;

        return base.BeforeCreate(data);
    }

    protected override Task BeforeUpdate(Company data, Company old)
    {
        data.AdminUserId = old.AdminUserId;

        return base.BeforeUpdate(data, old);
    }

    public async Task<Company> Upsert(CompanyModifyDto dto)
    {
        var user = userRetrieverService.GetCurrentUser();

        var company = await companyRepository.GetByUserIdOrDefaultAsync(user.Id);

        if (company is not null)
        {
            return await UpdateAsync(company.Id, dto);
        }

        return await CreateAsync(dto);
    }

    public async Task<int> GetCurrentUserCompanyId()
    {
        var user = userRetrieverService.GetCurrentUser();

        var company = await companyRepository.GetByUserIdOrDefaultAsync(user.Id);

        if (company is null)
        {
            throw new ValidationException(ErrorCodes.CompanyNotFound);
        }


        return company.Id;
    }

    public async Task<CompanyDto?> GetCurrentUsersCompany()
    {
        var user = userRetrieverService.GetCurrentUser();

        var company = await companyRepository.GetByUserIdOrDefaultAsync(user.Id);

        return Mapper.Map<CompanyDto?>(company);
    }

    public async Task<CompanyDetailDto> GetDetailedCompany(int companyId)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
        
        var company = await companyRepository.GetDetailedCompany(companyId);
        
        if (company is null)
        {
            throw new ValidationException(ErrorCodes.CompanyNotFound);
        }

        var dto = Mapper.Map<CompanyDetailDto>(company);
        
        if (httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            var supabaseId = httpContextAccessor.HttpContext.User.GetSupabaseId();
        
            var companyFollows = await accountRepository.GetCompanyFollows(supabaseId);
        
            dto.IsCurrentUserFollowing = companyFollows.Any(x => x.CompanyId == dto.Id);
        }
        
        return dto;
    }

    public Task<List<RatingResult>> GetAverageRatings(int? companyId)
    {
        return companyRepository.GetAverageRatings(companyId);
    }
}