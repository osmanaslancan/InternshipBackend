using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using InternshipBackend.Data;
using InternshipBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Quartz;

namespace InternshipBackend.Modules.App;

[DisallowConcurrentExecution]
public class NotificationSendJob(InternshipDbContext dbContext)  : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var queryable = from notification in dbContext.UserNotifications
            join user in dbContext.Users.AsNoTracking() on notification.UserId equals user.Id
            where notification.Status == UserNotification.NotificationStatus.Created
            select new
            {
                Notification = notification,
                User = user
            };
        
        var notifications = await queryable.ToListAsync();
        
        if (notifications.Count == 0)
            return;

        

        foreach (var notification in notifications)
        {
            if (notification.User.NotificationTokens == null || notification.User.NotificationTokens.Count == 0)
            {
                notification.Notification.Status = UserNotification.NotificationStatus.Failed;
                dbContext.UserNotifications.Update(notification.Notification);
                await dbContext.SaveChangesAsync();
                continue;
            }
            
            var sendNotifications = notification.User.NotificationTokens.Select(x => new Message()
            {
                Notification = new Notification()
                {
                    Title = notification.Notification.Title,
                    Body = notification.Notification.Body,
                },
                Token = x,
            });
            
            var response = await FirebaseMessaging.DefaultInstance.SendEachAsync(sendNotifications);

            if (response.SuccessCount > 0)
            {
                notification.Notification.Status = UserNotification.NotificationStatus.Sent;
                dbContext.UserNotifications.Update(notification.Notification);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}