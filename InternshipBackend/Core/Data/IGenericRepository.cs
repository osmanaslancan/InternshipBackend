namespace InternshipBackend.Core.Data;

public interface IGenericRepository<T>
    where T : class, IHasIdField
{
    public Task CreateAsync(T record, bool save = true);
    public Task UpdateAsync(T record, bool save = true);
    public Task DeleteAsync(T record, bool save = true);
    public Task<T?> GetByIdOrDefaultAsync(int id, bool changeTracking = true);
    public Task SaveChangesAsync();
}
