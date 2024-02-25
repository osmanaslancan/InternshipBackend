namespace InternshipBackend.Data.Seeds;

public interface ISeeder
{
    Task<bool> ShouldExecute(IServiceProvider serviceProvider);
    Task SeedAsync(IServiceProvider serviceProvider);
    Task PostSeed(IServiceProvider serviceProvider);
}
