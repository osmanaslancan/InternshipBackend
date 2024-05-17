using InternshipBackend.Data.Models;
using InternshipBackend.Data.Models.Supabase;
using InternshipBackend.Data.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Data;

public class InternshipDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserNotification> UserNotifications { get; set; }
    public DbSet<University> Universities { get; set; }
    public DbSet<DbSeed> DbSeeds { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<ForeignLanguage> ForeignLanguages { get; set; }
    public DbSet<UniversityEducation> UniversityEducations { get; set; }
    public DbSet<WorkHistory> WorkHistories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
    public DbSet<InternshipPosting> InternshipPostings { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }

    public DbSet<StorageObject> SupabaseStorageObjects { get; set; }

    public InternshipDbContext()
    {
    }

    public InternshipDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<string>().HaveMaxLength(255);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<StorageObject>().ToTable("storage.objects", (t) => t.ExcludeFromMigrations());

        modelBuilder.Entity<User>(b =>
        {
            b.HasOne(x => x.Detail)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDetail>(x => x.Id);

            b.HasIndex(x => x.Email)
                .IsUnique();

            b.HasIndex(x => x.SupabaseId)
                .IsUnique();

            b.HasMany<UserCompanyFollow>(x => x.FollowedCompanies).WithOne()
                .HasForeignKey(x => x.UserId);

            b.HasMany<UserPostingFollow>(x => x.FollowedPostings).WithOne()
                .HasForeignKey(x => x.UserId);

            b.Property(x => x.NotificationTokens).HasColumnType("text[]");
        });

        modelBuilder.Entity<UserCompanyFollow>(b =>
        {
            b.HasKey(x => new { x.UserId, x.CompanyId });
            b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId);
        });
        modelBuilder.Entity<UserPostingFollow>(b =>
        {
            b.HasKey(x => new { x.UserId, x.PostingId });
            b.HasOne<InternshipPosting>().WithMany().HasForeignKey(x => x.PostingId);
        });

        modelBuilder.Entity<UniversityEducation>(b =>
        {
            b.Property(x => x.Description).HasMaxLength(1000);
            b.Property(x => x.StartDate).HasColumnType("date");
            b.Property(x => x.EndDate).HasColumnType("date");
            b.HasOne(x => x.University).WithMany().HasForeignKey(x => x.UniversityId);
        });

        modelBuilder.Entity<UserProject>(b =>
        {
            b.Property(x => x.ProjectName).HasMaxLength(255);
            b.Property(x => x.Description).HasMaxLength(1000);
            b.Property(x => x.ProjectThumbnail).HasMaxLength(255);
            b.Property(x => x.ProjectLink).HasMaxLength(255);
        });

        modelBuilder.Entity<WorkHistory>(b =>
        {
            b.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            b.Property(x => x.EndDate).HasColumnType("date");
            b.Property(x => x.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<UserDetail>(b =>
        {
            b.Property(x => x.DateOfBirth).HasColumnType("date");
            b.Property(x => x.Extras).HasColumnType("jsonb");
            b.Property(x => x.DriverLicenses).HasColumnType("text[]");
            b.OwnsMany(x => x.Cvs, d => { d.ToJson(); });
        });

        modelBuilder.Entity<UserPermission>(b => { b.HasKey(x => new { x.UserId, x.Permission }); });

        modelBuilder.Entity<UserReference>(b =>
        {
            b.Property(x => x.Description).HasMaxLength(500);
            b.HasOne<UserDetail>().WithMany(x => x.References).HasForeignKey(x => x.UserId);
            b.HasOne<User>().WithMany(x => x.References).HasForeignKey(x => x.UserId);
        });

        modelBuilder.Entity<Company>(b =>
        {
            b.Property(x => x.Description).HasMaxLength(2000);
            b.Property(x => x.ShortDescription).HasMaxLength(75);
            b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId);
            b.HasOne<City>().WithMany().HasForeignKey(x => x.CityId);
            b.HasOne<User>(x => x.AdminUser).WithOne().HasForeignKey<Company>(x => x.AdminUserId);
        });

        modelBuilder.Entity<InternshipPosting>(b =>
        {
            b.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId);
            b.Property(x => x.Description).HasMaxLength(2000).HasColumnType("text");
            b.HasMany<InternshipApplication>(x => x.Applications).WithOne().HasForeignKey(x => x.InternshipPostingId);
            b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId);
            b.HasOne<City>().WithMany().HasForeignKey(x => x.CityId);
            b.OwnsMany(x => x.Comments, d => { d.ToJson(); });
            b.HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "turkish",
                    p => new { p.Title, p.Description })
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
        });

        modelBuilder.Entity<InternshipApplication>(b =>
        {
            b.HasOne<User>().WithMany(x => x.Applications).HasForeignKey(x => x.UserId);
        });
        
        modelBuilder.Entity<UserNotification>(b =>
        {
            b.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
            b.Property(x => x.UserId).IsRequired();
        });
    }
}