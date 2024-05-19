namespace InternshipBackend.Modules.Internship;

public class InternshipApplicationInternListDto : InternshipApplicationDto
{
    public required InternshipPostingListDto Posting { get; set; }
}