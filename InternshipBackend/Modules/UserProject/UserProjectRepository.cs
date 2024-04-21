using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.UserProject;

public class UserProjectRepository(InternshipDbContext dbContext) 
    : GenericRepository<Data.Models.UserProject>(dbContext), 
    IGenericRepository<Data.Models.UserProject>
{
}