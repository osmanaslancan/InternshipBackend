using InternshipBackend.Core;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Data.Models;

public class ForeignLanguage : UserOwnedEntity
{
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
