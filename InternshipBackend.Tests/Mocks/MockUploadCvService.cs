using InternshipBackend.Modules.App;

namespace InternshipBackend.Tests.Mocks;

public class MockUploadCvService : IUploadCvService
{
    public Task<UploadResponse> UploadFile(UploadCvRequest request)
    {
        throw new NotImplementedException();
    }

    public string GetDownloadUrlForCurrentUser(Guid ownerId, Guid file)
    {
        throw new NotImplementedException();
    }
}