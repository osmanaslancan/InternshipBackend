using InternshipBackend.Core.Services;
using InternshipBackend.Data;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.AccountDetail;

[Route("UserProject/[action]")]
public class UserProjectEndpoint(IUserProjectService userProjectService) 
    : CrudEndpoint<UserProjectDto, UserProject>(userProjectService)
{
}
