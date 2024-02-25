using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace InternshipBackend.Data.Seeds;


public abstract class SeederBase : ISeeder
{
    public async Task<bool> ShouldExecute(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<InternshipDbContext>();

        if (await context.DbSeeds.AnyAsync(x => x.SeederName == GetType().Name))
            return false;

        return true;
    }

    protected async Task<T> GetRequiredJsonResourceAsync<T>(string name)
    {
        var dataStream = GetType().Assembly.GetManifestResourceStream($"InternshipBackend.Data.Seeds.{name}.json");
        ArgumentNullException.ThrowIfNull(dataStream, nameof(dataStream));

        var data = await JsonSerializer.DeserializeAsync<T>(dataStream);

        ArgumentNullException.ThrowIfNull(data, nameof(data));

        return data;
    }

    public abstract Task SeedAsync(IServiceProvider serviceProvider);

    public Task PostSeed(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<InternshipDbContext>();
        context.DbSeeds.Add(new DbSeed { SeederName = GetType().Name, AppliedAt = DateTime.UtcNow });
        return context.SaveChangesAsync();
    }
}
