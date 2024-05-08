using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UniversityEducations;

[Route("UniversityEducation")]
[Authorize(PermissionKeys.Intern)]
public class UniversityEducationEndpoint(IUniversityEducationService userDetailService) 
    : CrudEndpoint<UniversityEducationModifyDto, UniversityEducation>(userDetailService)
{
}
