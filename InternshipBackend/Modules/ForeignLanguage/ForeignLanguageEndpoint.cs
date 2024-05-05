using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.ForeignLanguage;

[Route("ForeignLanguage")]
public class ForeignLanguageEndpoint(IForeignLanguageService foreignLanguageService) 
    : CrudEndpoint<ForeignLanguageModifyDto, Data.Models.ForeignLanguage>(foreignLanguageService)
{
}
