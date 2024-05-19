using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data.Models;

namespace InternshipBackend.Modules.UserProjects;

public interface IUserProjectService : IGenericEntityService<UserProjectModifyDto, UserProject>
{
    Task<UserProject> UpdateThumbnail(int id, string? url);
}

public class UserProjectService(IServiceProvider serviceProvider, IHttpContextAccessor contextAccessor, IConfiguration configuration)
    : GenericEntityService<UserProjectModifyDto, Data.Models.UserProject>(serviceProvider), IUserProjectService
{
    public async Task<UserProject> UpdateThumbnail(int id, string? url)
    {
        var old = await Repository.GetByIdOrDefaultAsync(id, changeTracking: false) ?? throw new Exception("Record not found");
        await ValidateOwnedByCurrentUser(old);
        
        ArgumentNullException.ThrowIfNull(contextAccessor.HttpContext);
        
        var supabaseId = contextAccessor.HttpContext.User.GetSupabaseId();
        if (url != null && !url.StartsWith($"{configuration["SupabaseStorageBaseUrl"]}/public/PublicImages/{supabaseId}/"))
        {
            throw new ValidationException("Invalid project thumbnail prefix.");
        }

        old.ProjectThumbnail = url;

        await Repository.UpdateAsync(old);
        
        return old;
    }
}