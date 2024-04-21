using InternshipBackend.Core.Services;

namespace InternshipBackend.Modules.ForeignLanguage;

public interface IForeignLanguageService : IGenericEntityService<ForeignLanguageModifyDto, Data.Models.ForeignLanguage>
{
}

public class ForeignLanguageService(IServiceProvider serviceProvider)
    : GenericEntityService<ForeignLanguageModifyDto, Data.Models.ForeignLanguage>(serviceProvider), IForeignLanguageService
{
}