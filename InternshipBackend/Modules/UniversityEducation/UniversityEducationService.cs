using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IUniversityEducationService : IGenericService<UniversityEducationDTO, UniversityEducationDTO, DeleteRequest, UniversityEducation>
{
}

public class UniversityEducationService(IServiceProvider serviceProvider, IUserRetriverService userRetriver)
    : GenericService<UniversityEducationDTO, UniversityEducationDTO, DeleteRequest, UniversityEducation>(serviceProvider), IUniversityEducationService
{
    protected override Task BeforeCreate(UniversityEducation data)
    {
        var user = userRetriver.GetCurrentUser();
        data.UserId = user.UserId;

        return base.BeforeCreate(data);
    }

    protected override async Task BeforeUpdate(UniversityEducation data)
    {
        var user = userRetriver.GetCurrentUser();

        var old = (await _repository.GetByIdOrDefaultAsync(data.UniversityEducationId)) ?? throw new Exception("Record not found");

        if (old.UserId != data.UserId || old.UserId != user.UserId)
        {
            throw new Exception("You can't update other user's data");
        }
    }

    protected override Task BeforeDelete(UniversityEducation data)
    {
        var user = userRetriver.GetCurrentUser();

        if (data.UserId != user.UserId)
        {
            throw new Exception("You can't delete other user's data");
        }

        return base.BeforeDelete(data);
    }

    protected override UniversityEducation MapDelete(DeleteRequest data)
    {
        var result = Activator.CreateInstance<UniversityEducation>();
        result.UniversityEducationId = (int)data.Id;

        return result;
    }
}