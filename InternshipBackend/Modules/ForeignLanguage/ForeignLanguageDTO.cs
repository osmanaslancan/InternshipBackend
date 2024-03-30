using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(ForeignLanguage), ReverseMap = true)]
public class ForeignLanguageDto
{
    public required string LanguageCode { get; set; }
    public LanguageDegree Degree { get; set; }
}
