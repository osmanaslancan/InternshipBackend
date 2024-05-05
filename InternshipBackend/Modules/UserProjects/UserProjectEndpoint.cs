using InternshipBackend.Core.Services;
using InternshipBackend.Modules.App;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UserProjects;

[Route("UserProject")]
public class UserProjectEndpoint(IUserProjectService userProjectService, IUploadImageService uploadImageService)
    : CrudEndpoint<UserProjectModifyDto, Data.Models.UserProject>(userProjectService)
{
    [HttpPut("Update/{id:int}")]
    public async Task<UploadImageResponse> UpdateImageAsync([FromRoute] int id, UploadImageRequest request)
    {
        if (request.File is null)
        {
            await userProjectService.UpdateImage(id, null);
            return new UploadImageResponse()
            {
                Url = null
            };
        }

        var response = await uploadImageService.UploadImage(request);

        await userProjectService.UpdateImage(id, response.Url);

        return response;
    }
}