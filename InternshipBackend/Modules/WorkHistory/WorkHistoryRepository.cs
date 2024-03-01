using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public class WorkHistoryRepository(InternshipDbContext dbContext) 
    : GenericRepository<WorkHistory>(dbContext), 
    IGenericRepository<WorkHistory>
{
}