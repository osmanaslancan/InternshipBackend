using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.CompanyManagement;

public interface ICompanyService : IGenericEntityService<CompanyModifyDto, Data.Models.Company>
{
    Task<Company> Upsert(CompanyModifyDto dto);
}

public class CompanyService(IServiceProvider serviceProvider, 
    ICompanyRepository companyRepository,
    IUserRetrieverService userRetrieverService)
    : GenericEntityService<CompanyModifyDto, Data.Models.Company>(serviceProvider), ICompanyService
{
    public async Task<Company> Upsert(CompanyModifyDto dto)
    {
        var user = userRetrieverService.GetCurrentUser();

        var id = await companyRepository.GetIdByUserIdOrDefaultAsync(user.Id);

        if (id is not null)
        {
            return await UpdateAsync(id.Value, dto);
        }

        return await CreateAsync(dto);
    }
}