using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public class ForeignLanguageRepository(InternshipDbContext dbContext) 
    : GenericRepository<ForeignLanguage>(dbContext), 
    IGenericRepository<ForeignLanguage>
{
}