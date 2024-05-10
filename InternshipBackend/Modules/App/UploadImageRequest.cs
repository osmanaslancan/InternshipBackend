using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

public class UploadImageRequest
{
    public enum ImageType
    {
        Image,
        Background,
    }
    [ModelBinder(Name = "file")]
    public IFormFile? File { get; set; }
    [ModelBinder(Name = "type")]

    public ImageType Type { get; set; }
}