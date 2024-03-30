using AutoMapper;
using FluentValidation;
using InternshipBackend.Core.Data;

namespace InternshipBackend.Core.Services;

public abstract class GenericService<TCreate, TUpdate, TDelete, TData>(IServiceProvider serviceProvider) 
    : BaseService, IGenericService<TCreate, TUpdate, TDelete, TData>, IService
    where TData : class, IHasIdField
{
    protected readonly IGenericRepository<TData> _repository = serviceProvider.GetRequiredService<IGenericRepository<TData>>();
    protected readonly IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
    protected readonly IUserRetriverService userRetriver = serviceProvider.GetRequiredService<IUserRetriverService>();

    protected virtual void ValidateCreate(TCreate data)
    {
        serviceProvider.GetService<IValidator<TCreate>>()?.ValidateAndThrow(data);
    }

    protected virtual TData MapCreate(TCreate data)
    {
        return mapper.Map<TData>(data);
    }

    protected virtual Task BeforeCreate(TData data) 
    {
        // Clear assignment of Id
        data.Id = 0;

        if (data is IHasUserIdField userField)
        {
            var user = userRetriver.GetCurrentUser();
            userField.UserId = user.Id;
        }

        return Task.CompletedTask;
    }

    protected virtual Task BeforeUpdate(TData data, TData old) 
    {
        if (data is IHasUserIdField userField)
        {
            var oldUserField = (IHasUserIdField)old;
            var user = userRetriver.GetCurrentUser();

            if (oldUserField.UserId != userField.UserId || oldUserField.UserId != user.Id)
            {
                throw new Exception("You can't update other user's data");
            }
        }

        return Task.CompletedTask;
    }
    
    protected virtual Task BeforeDelete(TData data) 
    {
        if (data is IHasUserIdField userField)
        {
            var user = userRetriver.GetCurrentUser();

            if (userField.UserId != user.Id)
            {
                throw new Exception("You can't delete other user's data");
            }
        }
        return Task.CompletedTask;
    }

    protected virtual void ValidateUpdate(TUpdate data)
    {
        serviceProvider.GetService<IValidator<TUpdate>>()?.ValidateAndThrow(data);
    }

    protected virtual TData MapUpdate(TUpdate data)
    {
        return mapper.Map<TData>(data);
    }

    protected virtual void ValidateDelete(TDelete data)
    {
        serviceProvider.GetService<IValidator<TDelete>>()?.ValidateAndThrow(data);
    }

    protected virtual async Task<TData> FindOld(TData data)
    {
        return await _repository.GetByIdOrDefaultAsync(data.Id) ?? throw new Exception("Record not found");
    }

    protected virtual TData MapDelete(TDelete data)
    {
        if (data is DeleteRequest deleteRequest)
        {
            var result = Activator.CreateInstance<TData>();
            result.Id = (int)deleteRequest.Id;
            return result;
        }
        return mapper.Map<TData>(data);
    }


    public virtual async Task CreateAsync(TCreate data)
    {
        ValidateCreate(data);
        var record = MapCreate(data);
        await BeforeCreate(record);
        await _repository.CreateAsync(record);
    }

    public virtual async Task UpdateAsync(TUpdate data)
    {
        ValidateUpdate(data);
        var record = MapUpdate(data);
        var old = await FindOld(record);
        await BeforeUpdate(record, old);
        await _repository.UpdateAsync(record);
    }

    public virtual async Task DeleteAsync(TDelete data)
    {
        ValidateDelete(data);
        var record = MapDelete(data);
        await BeforeDelete(record);
        await _repository.DeleteAsync(record);
    }
}
