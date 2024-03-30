using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(ForeignLanguage))]
public class ForeignLanguageListDto : ForeignLanguageDto
{
    public int Id { get; set; }
}
