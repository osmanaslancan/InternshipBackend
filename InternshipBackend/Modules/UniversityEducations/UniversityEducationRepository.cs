using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.UniversityEducations;

public class UniversityEducationRepository(InternshipDbContext dbContext) 
    : GenericRepository<UniversityEducation>(dbContext), 
    IGenericRepository<UniversityEducation>
{
}