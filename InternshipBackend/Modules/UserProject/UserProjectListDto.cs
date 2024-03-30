using AutoMapper;

namespace InternshipBackend.Data;

[AutoMap(typeof(UserProject))]
public class UserProjectDtoListDto : UserProjectDto
{
    public int Id { get; set; }
}
