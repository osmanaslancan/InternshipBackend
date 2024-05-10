namespace InternshipBackend.Modules.App;

public interface IUploadImageService
{
    Task<UploadResponse> UploadImage(UploadImageRequest request);
    bool IsOwnedByCurrentUser(string url);
}