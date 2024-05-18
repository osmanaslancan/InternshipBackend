
namespace InternshipBackend.Modules.Account;

public class UserNotificationDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public DateTime CreatedAt { get; set; }
}