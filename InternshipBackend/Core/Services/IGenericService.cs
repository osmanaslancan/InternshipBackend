namespace InternshipBackend.Core.Services;

public interface IGenericService<TDto, TData>
    where TData : class
{
    Task CreateAsync(TDto data);
    Task UpdateAsync(int id, TDto data);
    Task DeleteAsync(int id);
}
