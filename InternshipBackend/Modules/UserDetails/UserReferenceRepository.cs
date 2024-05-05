using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.UserDetails;

public class UserReferenceRepositoryRepository(InternshipDbContext dbContext) 
    : GenericRepository<UserReference>(dbContext), 
        IGenericRepository<UserReference>
{
}