using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("ForeignLanguage/[action]")]
public class ForeignLanguageEndpoint(IForeignLanguageService foreignLanguageService) 
    : CrudEndpoint<ForeignLanguageDto, ForeignLanguage>(foreignLanguageService)
{
}
