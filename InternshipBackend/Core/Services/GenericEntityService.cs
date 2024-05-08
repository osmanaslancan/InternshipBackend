using AutoMapper;
using FluentValidation;
using InternshipBackend.Core.Data;

namespace InternshipBackend.Core.Services;

public abstract class GenericEntityService<TDto, TData>(IServiceProvider serviceProvider) 
    : BaseService, IGenericEntityService<TDto, TData>, IScopedService
    where TData : class, IHasIdField
{
    protected readonly IGenericRepository<TData> _repository = serviceProvider.GetRequiredService<IGenericRepository<TData>>();
    protected readonly IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
    protected readonly IUserRetrieverService UserRetriever = serviceProvider.GetRequiredService<IUserRetrieverService>();
    
    protected virtual Task BeforeCreate(TData data) 
    {
        if (data is IHasUserIdField userField)
        {
            var user = UserRetriever.GetCurrentUser();
            userField.UserId = user.Id;
        }

        return Task.CompletedTask;
    }

    protected virtual Task ValidateOwnedByCurrentUser(IHasUserIdField data)
    {
        var user = UserRetriever.GetCurrentUser();
            
        if (data.UserId != user.Id)
        {
            throw new Exception("You can't update other user's data");
        }

        return Task.CompletedTask;
    }
    
    protected virtual Task BeforeUpdate(TData data, TData old) 
    {
        if (data is IHasUserIdField userField)
        {
            var oldUserField = (IHasUserIdField)old;
            
            ValidateOwnedByCurrentUser(oldUserField);
            
            userField.UserId = oldUserField.UserId;
        }

        return Task.CompletedTask;
    }
    
    protected virtual Task BeforeDelete(TData data) 
    {
        if (data is IHasUserIdField userField)
        {
            ValidateOwnedByCurrentUser(userField);
        }
        return Task.CompletedTask;
    }

    protected virtual void ValidateDto(TDto data)
    {
        serviceProvider.GetService<IValidator<TDto>>()?.ValidateAndThrow(data);
    }

    protected virtual TData MapDto(TDto data)
    {
        return mapper.Map<TDto, TData>(data);
    }

    public virtual async Task<TData> CreateAsync(TDto data)
    {
        ValidateDto(data);
        var record = MapDto(data);
        await BeforeCreate(record);
        return await _repository.CreateAsync(record);
    }

    public virtual async Task<TData> UpdateAsync(int id, TDto dto)
    {
        ValidateDto(dto);
        var newData = MapDto(dto);
        newData.Id = id;
        var old = await _repository.GetByIdOrDefaultAsync(id, changeTracking: false) ?? throw new Exception("Record not found");
        await BeforeUpdate(newData, old);
        return await _repository.UpdateAsync(newData);
    }

    public virtual async Task<TData> DeleteAsync(int id)
    {
        var data = await _repository.GetByIdOrDefaultAsync(id, changeTracking: false) ?? throw new Exception("Record not found");
        await BeforeDelete(data);
        return await _repository.DeleteAsync(data);
    }
}
