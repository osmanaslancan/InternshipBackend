using InternshipBackend.Core.Data;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public class UniversityEducationRepository(InternshipDbContext dbContext) 
    : GenericRepository<UniversityEducation>(dbContext), 
    IGenericRepository<UniversityEducation>
{
}