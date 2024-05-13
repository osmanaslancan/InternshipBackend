using InternshipBackend.Core;
using InternshipBackend.Modules.UniversityEducations;
using InternshipBackend.Modules.WorkHistory;

namespace InternshipBackend.Modules.Account;

public class LinkedinScrapeResponse : ServiceResponse
{
    public List<UniversityEducationListDto> Educations { get; set; } = new();
    public List<WorkHistoryListDto> WorkHistory { get; set; } = new();
}