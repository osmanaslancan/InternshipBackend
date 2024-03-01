namespace InternshipBackend.Core.Services;

public interface IGenericService<TCreate, TUpdate, TDelete, TData>
    where TData : class
{
    Task CreateAsync(TCreate data);
    Task UpdateAsync(TUpdate data);
    Task DeleteAsync(TDelete data);
}
