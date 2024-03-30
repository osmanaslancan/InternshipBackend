using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules;

public class UpdateProfileImageRequest
{
    [ModelBinder(Name = "file")]
    public required IFormFile File { get; set; }
}   