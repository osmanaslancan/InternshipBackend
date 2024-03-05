namespace InternshipBackend.Core.Data;

public interface IGenericRepository<T>
    where T : class
{
    public Task CreateAsync(T record, bool save = true);
    public Task UpdateAsync(T record, bool save = true);
    public Task DeleteAsync(T record, bool save = true);
    public Task<T?> GetByIdOrDefaultAsync(object id);
    public Task SaveChangesAsync();
}
