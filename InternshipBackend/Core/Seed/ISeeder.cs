namespace InternshipBackend.Core.Seed;

public interface ISeeder
{
    Task<bool> ShouldExecute(IServiceProvider serviceProvider);
    Task SeedAsync(IServiceProvider serviceProvider);
    Task PostSeed(IServiceProvider serviceProvider);
}
