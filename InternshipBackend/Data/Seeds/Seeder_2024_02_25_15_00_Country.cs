namespace InternshipBackend.Data.Seeds;

[Seeder(2024_02_25_15_00)]
public class Seeder_2024_02_25_15_00_Country : SeederBase
{
    public override async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var data = await GetRequiredJsonResourceAsync<List<Country>>("Countries");

        var context = serviceProvider.GetRequiredService<InternshipDbContext>();

        await context.Countries.AddRangeAsync(data);
    }
}
