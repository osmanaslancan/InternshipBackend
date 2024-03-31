namespace InternshipBackend.Core.Data;

public interface IGenericRepository<T>
    where T : class, IHasIdField
{
    public Task<T> CreateAsync(T record, bool save = true);
    public Task<T> UpdateAsync(T record, bool save = true);
    public Task<T> DeleteAsync(T record, bool save = true);
    public Task<T?> GetByIdOrDefaultAsync(int id, bool changeTracking = true);
    public Task SaveChangesAsync();
}
