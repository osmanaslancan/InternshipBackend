using AutoMapper;
using FluentValidation;
using InternshipBackend.Core.Data;

namespace InternshipBackend.Core.Services;

public abstract class GenericService<TCreate, TUpdate, TDelete, TData>
    (IServiceProvider serviceProvider) 
    : BaseService, IGenericService<TCreate, TUpdate, TDelete, TData>, IService
    where TData : class
{
    protected readonly IGenericRepository<TData> _repository = serviceProvider.GetRequiredService<IGenericRepository<TData>>();
    protected readonly IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

    protected virtual void ValidateCreate(TCreate data)
    {
        serviceProvider.GetService<IValidator<TCreate>>()?.ValidateAndThrow(data);
    }

    protected virtual TData MapCreate(TCreate data)
    {
        return mapper.Map<TData>(data);
    }

    protected virtual Task BeforeCreate(TData data) { return Task.CompletedTask; }

    protected virtual Task BeforeUpdate(TData data) { return Task.CompletedTask; }
    
    protected virtual Task BeforeDelete(TData data) { return Task.CompletedTask; }

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

    protected virtual TData MapDelete(TDelete data)
    {
        return mapper.Map<TData>(data);
    }


    public virtual async Task CreateAsync(TCreate data)
    {
        ValidateCreate(data);
        var record = MapCreate(data);
        await BeforeCreate(record);
        await _repository.CreateAsync(record);
        await _repository.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TUpdate data)
    {
        ValidateUpdate(data);
        var record = MapUpdate(data);
        await BeforeUpdate(record);
        await _repository.UpdateAsync(record);
        await _repository.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TDelete data)
    {
        ValidateDelete(data);
        var record = MapDelete(data);
        await BeforeDelete(record);
        await _repository.DeleteAsync(record);
        await _repository.SaveChangesAsync();
    }
}
