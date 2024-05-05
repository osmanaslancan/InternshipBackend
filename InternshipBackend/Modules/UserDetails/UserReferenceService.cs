using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.UserDetails;

public interface IUserReferenceService : IGenericEntityService<UserReferenceModifyDto, UserReference>
{
}

public class UserReferenceService(IServiceProvider serviceProvider)
    : GenericEntityService<UserReferenceModifyDto, UserReference>(serviceProvider), IUserReferenceService
{
}