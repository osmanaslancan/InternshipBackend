using InternshipBackend.Core;
using OpenAI.ObjectModels.ResponseModels;

namespace InternshipBackend.Data.Models;

public class UserSuggestion : UserOwnedEntity
{
    public required DateTime CreatedAt { get; set; }
    public required string QuestionHash { get; set; }
    public string? Response { get; set; }
}