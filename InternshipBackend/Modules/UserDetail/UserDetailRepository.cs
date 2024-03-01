using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public class UserDetailRepository(InternshipDbContext dbContext) 
    : GenericRepository<UserDetail>(dbContext), 
    IGenericRepository<UserDetail>
{
}