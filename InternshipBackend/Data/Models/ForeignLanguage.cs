namespace InternshipBackend.Data;

public class ForeignLanguage
{
    public int UserId { get; set; }
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
