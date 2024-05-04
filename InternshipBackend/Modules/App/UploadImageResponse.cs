using InternshipBackend.Core;

namespace InternshipBackend.Modules.App;

public class UploadImageResponse : ServiceResponse
{
    public required string Url { get; set; }
}