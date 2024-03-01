using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public class UserProjectRepository(InternshipDbContext dbContext) 
    : GenericRepository<UserProject>(dbContext), 
    IGenericRepository<UserProject>
{
}