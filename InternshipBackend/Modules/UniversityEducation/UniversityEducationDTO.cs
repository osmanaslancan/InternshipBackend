using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UniversityEducation))]
public class UniversityEducationDTO
{
    public int Id { get; set; }
    public int? UniversityId { get; set; }
    public string? UniversityName { get; set; }
    public string? Faculty { get; set; }
    public required string Department { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsGraduated { get; set; }
    public double GPA { get; set; }
    public string? Description { get; set; }
}
