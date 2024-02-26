using InternshipBackend.Core.Seed;

namespace InternshipBackend.Data.Seeds;

[Seeder(2024_02_24_00_00)]
public class Seeder_2024_02_24_00_00_University : SeederBase
{
    public override async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var data = await GetRequiredJsonResourceAsync<List<University>>("Universities");

        var context = serviceProvider.GetRequiredService<InternshipDbContext>();

        await context.Universities.AddRangeAsync(data);
    }
}
