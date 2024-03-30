using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UniversityEducation))]
public class UniversityEducationListDto : UniversityEducationDto
{
    public int Id { get; set; }
}
