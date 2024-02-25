using System.Reflection;

namespace InternshipBackend.Data.Seeds;

public class SeederManager
{
    public async Task ExecuteAsync(IServiceProvider serviceProvider)
    {
        var seeders = GetType().Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(ISeeder).IsAssignableFrom(t))
            .ToList();

        seeders = seeders.OrderBy(x => x.GetCustomAttribute<SeederAttribute>()?.Date ?? throw new InvalidDataException("SeederAttribute in seeders is required!")).ToList();

        foreach (var seeder in seeders)
        {
            var instance = (ISeeder)Activator.CreateInstance(seeder)!;
            var scope = serviceProvider.CreateScope();
            if (await instance.ShouldExecute(scope.ServiceProvider))
            {
                await instance.SeedAsync(scope.ServiceProvider);
                await instance.PostSeed(scope.ServiceProvider);
            }
        }
    }
}
