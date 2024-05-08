
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.UniversityEducations;

public class UniversityEducationModifyDto
{
    public int? UniversityId { get; set; }
    public string? UniversityName { get; set; }
    public string? Faculty { get; set; }
    public required string Department { get; set; }
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }
    
    public int? EducationYear { get; set; }
    public bool IsGraduated { get; set; }
    public double GPA { get; set; }
    public string? Description { get; set; }
}
