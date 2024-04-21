using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.WorkHistory;

public class WorkHistoryRepository(InternshipDbContext dbContext) 
    : GenericRepository<Data.Models.WorkHistory>(dbContext), 
    IGenericRepository<Data.Models.WorkHistory>
{
}