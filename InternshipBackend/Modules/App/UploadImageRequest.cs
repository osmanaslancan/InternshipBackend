using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

public class UploadImageRequest
{
    [ModelBinder(Name = "file")]
    public required IFormFile File { get; set; }
}