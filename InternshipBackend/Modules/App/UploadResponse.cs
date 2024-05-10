using InternshipBackend.Core;

namespace InternshipBackend.Modules.App;

public class UploadResponse : ServiceResponse
{
    public string? FileName { get; set; }
    public string? Url { get; set; }
}