using InternshipBackend.Core.Services;

namespace InternshipBackend.Modules.UserProject;

public interface IUserProjectService : IGenericEntityService<UserProjectModifyDto, Data.Models.UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider)
    : GenericEntityService<UserProjectModifyDto, Data.Models.UserProject>(serviceProvider), IUserProjectService
{
}