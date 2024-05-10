using System.Net.Http.Headers;
using FluentValidation;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace InternshipBackend.Modules.App;

public class UploadImageService(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory clientFactory,
    IConfiguration configuration) : UploadServiceBase(httpContextAccessor, clientFactory, configuration), IUploadImageService 
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IConfiguration _configuration = configuration;
    protected override string Bucket => "PublicImages";
    
    public async Task<UploadResponse> UploadImage(UploadImageRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.File);

        using var image = await SixLabors.ImageSharp.Image.LoadAsync(request.File.OpenReadStream());
        if (request.Type == UploadImageRequest.ImageType.Background)
        {
            image.Mutate(x => x.Resize(1920, 1080));
        }
        else
        {
            image.Mutate(x => x.Resize(512, 512));
        }
        using var resultStream = new MemoryStream();
        await image.SaveAsync(resultStream, new PngEncoder());
        
        return await Upload(resultStream.ToArray(), request.File.Name, request.File.FileName, request.File.ContentType);
    }

}