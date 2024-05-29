using InternshipBackend.Core;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Tests;

public class SqliteModelCustomizer : IDbContextModelCustomizer
{
    public void Customize(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InternshipPosting>(b => b.Ignore(x => x.SearchVector));
    }
}