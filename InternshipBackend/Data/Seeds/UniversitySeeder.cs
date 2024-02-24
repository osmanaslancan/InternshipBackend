using System.Runtime.InteropServices;
using System.Text.Json;

namespace InternshipBackend.Data.Seeds;

public class UniversitySeeder
{
    public async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<InternshipDbContext>();

        if (context.DbSeeds.Contains(new DbSeed { SeederName = nameof(UniversitySeeder) }))
            return;

        var dataStream = GetType().Assembly.GetManifestResourceStream("InternshipBackend.Data.Seeds.Universities.json");
        ArgumentNullException.ThrowIfNull(dataStream, nameof(dataStream));

        var data = await JsonSerializer.DeserializeAsync<List<University>>(dataStream);

        ArgumentNullException.ThrowIfNull(data, nameof(data));

        await context.Universities.AddRangeAsync(data);

        context.DbSeeds.Add(new DbSeed { SeederName = nameof(UniversitySeeder), AppliedAt = DateTime.UtcNow });

        await context.SaveChangesAsync();
    }
}
