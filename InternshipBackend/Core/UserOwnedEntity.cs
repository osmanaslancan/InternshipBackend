namespace InternshipBackend.Core;

public class UserOwnedEntity : Entity, IHasUserIdField
{
    public int UserId { get; set; }
}