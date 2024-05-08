using InternshipBackend.Core.Services;
using InternshipBackend.Modules.Account.Authorization;
using InternshipBackend.Modules.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipBackend.Modules.UserProjects;

[Route("UserProject")]
[Authorize(PermissionKeys.Intern)]
public class UserProjectEndpoint(IUserProjectService userProjectService, IUploadImageService uploadImageService)
    : CrudEndpoint<UserProjectModifyDto, Data.Models.UserProject>(userProjectService)
{
    [HttpPut("UpdateThumbnail/{id:int}")]
    public async Task<UploadImageResponse> UpdateThumbnailAsync([FromRoute] int id, UploadImageRequest request)
    {
        if (request.File is null)
        {
            await userProjectService.UpdateThumbnail(id, null);
            return new UploadImageResponse()
            {
                Url = null
            };
        }

        var response = await uploadImageService.UploadImage(request);

        await userProjectService.UpdateThumbnail(id, response.Url);

        return response;
    }
}