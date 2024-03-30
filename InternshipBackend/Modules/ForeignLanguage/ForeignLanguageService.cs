using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IForeignLanguageService : IGenericService<ForeignLanguageDTO, ForeignLanguageDTO, DeleteRequest, ForeignLanguage>
{
}

public class ForeignLanguageService(IServiceProvider serviceProvider)
    : GenericService<ForeignLanguageDTO, ForeignLanguageDTO, DeleteRequest, ForeignLanguage>(serviceProvider), IForeignLanguageService
{
}