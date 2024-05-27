using InternshipBackend.Core.Data;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.App;

public class NotificationRepository(InternshipDbContext dbContext) : 
    GenericRepository<UserNotification>(dbContext)
{
    public async Task CreateAsync(IEnumerable<UserNotification> notifications, bool save = true)
    {
        await DbContext.UserNotifications.AddRangeAsync(notifications);
        if (save)
            await DbContext.SaveChangesAsync();
    }
}