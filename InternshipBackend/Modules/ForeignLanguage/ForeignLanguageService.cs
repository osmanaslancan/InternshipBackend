using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IForeignLanguageService : IGenericEntityService<ForeignLanguageDto, ForeignLanguage>
{
}

public class ForeignLanguageService(IServiceProvider serviceProvider)
    : GenericEntityService<ForeignLanguageDto, ForeignLanguage>(serviceProvider), IForeignLanguageService
{
}