using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;

namespace InternshipBackend.Modules.UserProject;

public interface IUserProjectService : IGenericEntityService<UserProjectModifyDto, Data.Models.UserProject>
{
}

public class UserProjectService(IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor, IConfiguration configuration)
    : GenericEntityService<UserProjectModifyDto, Data.Models.UserProject>(serviceProvider), IUserProjectService
{
    protected override void ValidateDto(UserProjectModifyDto data)
    {
        base.ValidateDto(data);
        ArgumentNullException.ThrowIfNull(contextAccessor.HttpContext);
        
        var supabaseId = contextAccessor.HttpContext.User.GetSupabaseId();
        if (data.ProjectThumbnail != null && !data.ProjectThumbnail.StartsWith($"{configuration["SupabaseStorageBaseUrl"]}/public/PublicImages/{supabaseId}/"))
        {
            throw new ValidationException("Invalid project thumbnail prefix.");
        }
    }
}