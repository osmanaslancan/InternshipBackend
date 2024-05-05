using InternshipBackend.Core;

namespace InternshipBackend.Modules.App;

public class UploadImageResponse : ServiceResponse
{
    public string? Url { get; set; }
}