using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("UniversityEducation/[action]")]
public class UniversityEducationEndpoint(IUniversityEducationService userDetailService) 
    : CrudEndpoint<UniversityEducationDto, UniversityEducation>(userDetailService)
{
}
