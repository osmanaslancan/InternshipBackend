using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.Account;

public class UpdateProfileImageRequest
{
    [ModelBinder(Name = "file")]
    public required IFormFile File { get; set; }
}   