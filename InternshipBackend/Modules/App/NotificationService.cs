using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;
using InternshipBackend.Modules.Account;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Modules.App;

public class NotificationService(IAccountRepository accountRepository, NotificationRepository notificationRepository) : BaseService
{
    public async Task SendNotificationToCompanyFollowers(int companyId, string title, string body)
    {
        var users = await accountRepository.GetQueryable()
            .Where(x => x.FollowedCompanies.Any(x => x.CompanyId == companyId))
            .Select(x => x.Id).ToListAsync();

        if (!users.Any())
            return;
        var time = DateTime.UtcNow;        
        var notifications = users.Select(x => new UserNotification
        {
            UserId = x,
            Title = title,
            Body = body,
            Status = UserNotification.NotificationStatus.Created,
            CreatedAt = time
        }).ToList();

        await notificationRepository.CreateAsync(notifications);
    }
}