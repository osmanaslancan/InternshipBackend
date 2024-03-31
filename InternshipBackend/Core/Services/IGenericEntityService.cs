namespace InternshipBackend.Core.Services;

public interface IGenericEntityService<TDto, TData>
    where TData : class
{
    Task<TData> CreateAsync(TDto data);
    Task<TData> UpdateAsync(int id, TDto data);
    Task<TData> DeleteAsync(int id);
}
