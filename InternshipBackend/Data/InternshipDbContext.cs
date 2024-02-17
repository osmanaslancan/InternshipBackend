using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace InternshipBackend.Data;

public class InternshipDbContext : DbContext
{
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<DbSeed> DbSeeds { get; set; }

    public InternshipDbContext()
    {
    }

    public InternshipDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>()
            .HasIndex(x => x.SupabaseId)
            .IsUnique();
        modelBuilder.Entity<UserInfo>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}
