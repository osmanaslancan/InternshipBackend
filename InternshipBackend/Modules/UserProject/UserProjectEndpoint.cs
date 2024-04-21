using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UserProject;

[Route("UserProject/[action]")]
public class UserProjectEndpoint(IUserProjectService userProjectService) 
    : CrudEndpoint<UserProjectModifyDto, Data.Models.UserProject>(userProjectService)
{
}
