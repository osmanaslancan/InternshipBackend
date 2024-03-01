using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(ForeignLanguage))]
public class ForeignLanguageDTO
{
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
