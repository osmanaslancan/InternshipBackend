using InternshipBackend.Core;
using InternshipBackend.Core.Services;
using InternshipBackend.Data;

namespace InternshipBackend.Modules.AccountDetail;

public interface IForeignLanguageService : IGenericService<ForeignLanguageDTO, ForeignLanguageDTO, DeleteRequest, ForeignLanguage>
{
}

public class ForeignLanguageService(IServiceProvider serviceProvider, IUserRetriverService userRetriver)
    : GenericService<ForeignLanguageDTO, ForeignLanguageDTO, DeleteRequest, ForeignLanguage>(serviceProvider), IForeignLanguageService
{
    protected override Task BeforeCreate(ForeignLanguage data)
    {
        var user = userRetriver.GetCurrentUser();
        data.UserId = user.UserId;

        return base.BeforeCreate(data);
    }

    protected override async Task BeforeUpdate(ForeignLanguage data)
    {
        var user = userRetriver.GetCurrentUser();

        var old = (await _repository.GetByIdOrDefaultAsync(new int { })) ?? throw new Exception("Record not found");

        if (old.UserId != data.UserId || old.UserId != user.UserId)
        {
            throw new Exception("You can't update other user's data");
        }
    }

    protected override Task BeforeDelete(ForeignLanguage data)
    {
        var user = userRetriver.GetCurrentUser();

        if (data.UserId != user.UserId)
        {
            throw new Exception("You can't delete other user's data");
        }

        return base.BeforeDelete(data);
    }

    protected override ForeignLanguage MapDelete(DeleteRequest data)
    {
        var result = Activator.CreateInstance<ForeignLanguage>();
        result.UserId = userRetriver.GetCurrentUser().UserId;
        result.LanguageCode = (string)data.Id;

        return result;
    }
}