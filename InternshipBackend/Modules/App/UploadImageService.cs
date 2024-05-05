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
    IConfiguration configuration) : BaseService, IUploadImageService 
{
    public async Task<UploadImageResponse> UploadImage(UploadImageRequest request)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor.HttpContext);
        ArgumentNullException.ThrowIfNull(request.File);
        
        var userSupabaseId = httpContextAccessor.HttpContext.User.GetSupabaseId();

        using var image = await SixLabors.ImageSharp.Image.LoadAsync(request.File.OpenReadStream());
        image.Mutate(x => x.Resize(512, 512));
        using var resultStream = new MemoryStream();
        await image.SaveAsync(resultStream, new PngEncoder());
        
        var content = new MultipartFormDataContent();
        var streamContent = new ByteArrayContent(resultStream.ToArray());
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(request.File.ContentType);
        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = request.File.Name,
            FileName = request.File.FileName,
        };
        content.Add(streamContent);
        content.Headers.Add("X-Upsert", "true");
        using var client = clientFactory.CreateClient("Supabase");
        var guid = Guid.NewGuid();
        var url = $"{configuration["SupabaseStorageBaseUrl"]}/PublicImages/{userSupabaseId}/{guid}";
        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload image");
        }

        return new UploadImageResponse()
        {
            Url = $"{configuration["SupabaseStorageBaseUrl"]}/public/PublicImages/{userSupabaseId}/{guid}"
        };
    }
}