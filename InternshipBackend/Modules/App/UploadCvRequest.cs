using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

public class UploadCvRequest
{
    [ModelBinder(Name = "file")] 
    public IFormFile? File { get; set; }
}