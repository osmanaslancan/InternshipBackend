using InternshipBackend.Core.Seed;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Data.Seeds;

[Seeder(2024_02_25_15_23)]
public class Seeder_2024_02_25_15_23_City : SeederBase
{
    public override async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<InternshipDbContext>();

        var data = await GetRequiredJsonResourceAsync<Dictionary<string, string[]>>("Cities");
        var countries = (await context.Countries.Where(x => x.Code3 != null).ToListAsync()).ToLookup(x => x.Code3!.ToUpper(CultureInfo.InvariantCulture));

        var result = new List<City>();

        foreach (var (countryCode, cityNames) in data)
        {
            var country = countries[countryCode].FirstOrDefault();
            if (country == null)
                continue;

            result.AddRange(cityNames.Select(x => new City { Name = x, Country = country }));
        }

        await context.Cities.AddRangeAsync(result);
    }
}
