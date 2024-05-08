using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.ForeignLanguage;

[Route("ForeignLanguage")]
[Authorize(PermissionKeys.Intern)]
public class ForeignLanguageEndpoint(IForeignLanguageService foreignLanguageService) 
    : CrudEndpoint<ForeignLanguageModifyDto, Data.Models.ForeignLanguage>(foreignLanguageService)
{
}
