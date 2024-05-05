using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.UserDetails;

public class UserDetailRepository(InternshipDbContext dbContext) 
    : GenericRepository<Data.Models.UserDetail>(dbContext), 
    IGenericRepository<Data.Models.UserDetail>
{
}