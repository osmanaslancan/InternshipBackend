namespace InternshipBackend.Core.Data;

public interface IGenericRepository<T>
    where T : class
{
    public Task CreateAsync(T record);
    public Task UpdateAsync(T record);
    public Task DeleteAsync(T record);
    public Task<T?> GetByIdOrDefaultAsync(object id);
    public Task SaveChangesAsync();
}
