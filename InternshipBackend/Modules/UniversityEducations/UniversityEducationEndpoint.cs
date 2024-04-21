using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UniversityEducations;

[Route("UniversityEducation/[action]")]
public class UniversityEducationEndpoint(IUniversityEducationService userDetailService) 
    : CrudEndpoint<UniversityEducationModifyDto, UniversityEducation>(userDetailService)
{
}
