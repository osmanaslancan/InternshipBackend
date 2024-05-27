using System.Net.Http.Headers;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;

namespace InternshipBackend.Modules.App;

public abstract class UploadServiceBase(
    IHttpContextAccessor httpContextAccessor,
    IHttpClientFactory clientFactory,
    IConfiguration configuration) : BaseService
{
    protected readonly IConfiguration Configuration = configuration;
    protected readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;
    protected abstract string Bucket { get; }

    protected virtual string FilePostfix(Guid userSupabaseId, Guid guid)
    {
        return $"{Bucket}/{userSupabaseId}/{guid}";
    }

    protected virtual string UploadDirectory(Guid userSupabaseId, Guid guid)
    {
        return $"{Configuration["SupabaseStorageBaseUrl"]}/{FilePostfix(userSupabaseId, guid)}";
    }

    protected virtual string DownloadDirectory(Guid userSupabaseId, Guid guid)
    {
        return $"{Configuration["SupabaseStorageBaseUrl"]}/public/{FilePostfix(userSupabaseId, guid)}";
    }
    
    public virtual bool IsOwnedByCurrentUser(string url)
    {
        ArgumentNullException.ThrowIfNull(HttpContextAccessor.HttpContext);
        
        var supabaseId = HttpContextAccessor.HttpContext.User.GetSupabaseId();
        return url.StartsWith($"{Configuration["SupabaseStorageBaseUrl"]}/public/{Bucket}/{supabaseId}/");
    }

    protected async Task<UploadResponse> Upload(byte[] file, string name, string fileName, string contentType)
    {
        ArgumentNullException.ThrowIfNull(HttpContextAccessor.HttpContext);

        var userSupabaseId = HttpContextAccessor.HttpContext.User.GetSupabaseId();
        
        var content = new MultipartFormDataContent();
        var streamContent = new ByteArrayContent(file);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = name,
            FileName = fileName,
        };
        content.Add(streamContent);
        content.Headers.Add("X-Upsert", "true");
        using var client = clientFactory.CreateClient("Supabase");
        var guid = Guid.NewGuid();
        var url = UploadDirectory(userSupabaseId, guid);
        var response = await client.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload file");
        }

        return new UploadResponse()
        {
            FileName = fileName,
            Url = DownloadDirectory(userSupabaseId, guid)
        };
    }
}