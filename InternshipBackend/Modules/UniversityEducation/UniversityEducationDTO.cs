#nullable disable
using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UniversityEducation), ReverseMap = true)]
public class UniversityEducationDto
{
    public int? UniversityId { get; set; }
    public string? UniversityName { get; set; }
    public string? Faculty { get; set; }
    public string Department { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsGraduated { get; set; }
    public double GPA { get; set; }
    public string? Description { get; set; }
}
