using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.ForeignLanguage;

public class ForeignLanguageRepository(InternshipDbContext dbContext) 
    : GenericRepository<Data.Models.ForeignLanguage>(dbContext), 
    IGenericRepository<Data.Models.ForeignLanguage>
{
}