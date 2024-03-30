using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IForeignLanguageService : IGenericService<ForeignLanguageDto, ForeignLanguage>
{
}

public class ForeignLanguageService(IServiceProvider serviceProvider)
    : GenericService<ForeignLanguageDto, ForeignLanguage>(serviceProvider), IForeignLanguageService
{
}