using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserProjectService : IGenericService<UserProjectDto, UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider)
    : GenericService<UserProjectDto, UserProject>(serviceProvider), IUserProjectService
{
}