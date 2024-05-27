using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class UserNotification : UserOwnedEntity
{
    public enum NotificationStatus
    {
        Created,
        Sent,
        Failed
    }
    
    public required string Title { get; set; }
    public required string Body { get; set; }
    public NotificationStatus Status { get; set; }
    public required DateTime CreatedAt { get; set; }
}