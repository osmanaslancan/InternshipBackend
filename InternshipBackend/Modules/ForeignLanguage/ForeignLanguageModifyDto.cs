using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Modules.ForeignLanguage;

public class ForeignLanguageModifyDto
{
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
