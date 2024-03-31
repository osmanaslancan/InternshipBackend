using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserProjectService : IGenericEntityService<UserProjectDto, UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider)
    : GenericEntityService<UserProjectDto, UserProject>(serviceProvider), IUserProjectService
{
}