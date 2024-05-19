using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
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
        var company = await companyRepository.GetDetailedCompany(companyId);

        if (company is null)
        {
            throw new ValidationException(ErrorCodes.CompanyNotFound);
        }

        return Mapper.Map<CompanyDetailDto>(company);
    }

    public Task<List<RatingResult>> GetAverageRatings(int? companyId)
    {
        return companyRepository.GetAverageRatings(companyId);
    }
}