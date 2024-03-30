using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUserProjectService : IGenericService<UserProjectDTO, UserProjectDTO, DeleteRequest, UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider)
    : GenericService<UserProjectDTO, UserProjectDTO, DeleteRequest, UserProject>(serviceProvider), IUserProjectService
{
}