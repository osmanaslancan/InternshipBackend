using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(WorkHistory))]
public class WorkHistoryListDto : WorkHistoryDto
{
    public int Id { get; set; }
}
