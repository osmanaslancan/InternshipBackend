using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.App;

[Route("App")]
public class UploadEndpoint(IUploadImageService uploadImageService, IUploadCvService uploadCvService) : BaseEndpoint
{

    [HttpPost("UploadImage")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<UploadResponse>> UploadImage([FromForm] UploadImageRequest request)
    {
        var result = await uploadImageService.UploadImage(request);
       
        return Ok(result);
    }    
    
    [HttpPost("UploadCv")]
    [Consumes("multipart/form-data")]
    [Authorize(PermissionKeys.Intern)]
    public async Task<ActionResult<UploadResponse>> UploadCv([FromForm] UploadCvRequest request)
    {
        var result = await uploadCvService.UploadFile(request);
       
        return Ok(result);
    }    
}