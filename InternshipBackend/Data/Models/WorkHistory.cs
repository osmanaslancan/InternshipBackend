using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class WorkHistory : IHasUserIdField, IHasIdField
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Position { get; set; }
    public required string CompanyName { get; set; }
    public required DateTime StartDate { get; set; }
    public DateTime EndDate { get; set;}
    public bool IsWorkingNow { get; set; }
    public string? Description { get; set; }
    public string? Duties { get; set; }
    public WorkType WorkType { get; set; }
    public string? ReasonForLeave { get; set; }
}
