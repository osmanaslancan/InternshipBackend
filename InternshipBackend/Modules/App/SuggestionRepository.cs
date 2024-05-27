using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.App;

public class SuggestionRepository(InternshipDbContext dbContext) 
    : GenericRepository<UserSuggestion>(dbContext)
{
}