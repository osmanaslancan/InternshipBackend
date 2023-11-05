using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Data;

public class InternshipDbContext : DbContext
{
    public InternshipDbContext()
    {
    }

    public InternshipDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
