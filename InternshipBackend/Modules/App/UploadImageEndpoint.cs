using InternshipBackend.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

[Route("App")]
public class UploadImageEndpoint : BaseEndpoint
{
    private readonly IUploadImageService _uploadImageService;

    public UploadImageEndpoint(IUploadImageService uploadImageService)
    {
        _uploadImageService = uploadImageService;
    }

    [HttpPost("UploadImage")]
    public async Task<ActionResult<UploadImageResponse>> UploadImage([FromForm] UploadImageRequest request)
    {
        var result = await _uploadImageService.UploadImage(request);
       
        return Ok(result);
    }    
}