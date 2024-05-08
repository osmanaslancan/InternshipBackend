namespace InternshipBackend.Modules.App;

public interface IUploadImageService
{
    Task<UploadImageResponse> UploadImage(UploadImageRequest request);
    bool IsOwnedByCurrentUser(string url);
}