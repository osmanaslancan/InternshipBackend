using InternshipBackend.Core;

namespace InternshipBackend.Data;

public class ForeignLanguage : IHasUserIdField, IHasIdField
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
