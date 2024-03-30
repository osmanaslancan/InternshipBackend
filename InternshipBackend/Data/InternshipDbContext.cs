using InternshipBackend.Data.Models;
using InternshipBackend.Data.Supabase;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Data;

public class InternshipDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<DbSeed> DbSeeds { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<ForeignLanguage> ForeignLanguages { get; set; }
    public DbSet<UniversityEducation> UniversityEducations { get; set; }
    public DbSet<WorkHistory> WorkHistories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<DriverLicense> DriverLicenses { get; set; }
    public DbSet<City> Cities { get; set; }

    public DbSet<StorageObject> SupabaseStorageObjects { get; set; }

    public InternshipDbContext()
    {
    }

    public InternshipDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(x => x.SupabaseId)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<StorageObject>().ToTable("storage.objects", (t) => t.ExcludeFromMigrations());

        modelBuilder.Entity<User>()
            .HasOne(x => x.Detail)
            .WithOne(ud => ud.User)
            .HasForeignKey<UserDetail>(x => x.Id);
    }
}
