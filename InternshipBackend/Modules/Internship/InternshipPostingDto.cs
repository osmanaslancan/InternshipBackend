namespace InternshipBackend.Modules.Internship;

public class InternshipPostingDto : InternshipPostingListDto
{
    public required List<InternshipPostingCommentDto> Comments { get; set; }
    public double AveragePoint { get; set; }
    public int NumberOfComments { get; set; }
    public int NumberOfApplications { get; set; }
}