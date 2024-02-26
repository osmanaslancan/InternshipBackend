using InternshipBackend.Data;

namespace InternshipBackend.Core.Data;

public abstract class GenericRepository<T>(InternshipDbContext dbContext) : BaseRepository
    where T : class
{
    protected readonly InternshipDbContext dbContext = dbContext;

    public virtual async Task CreateAsync(T record)
    {
        await dbContext.AddAsync(record);
    }

    public virtual Task Update(T record)
    {
        dbContext.Update(record);

        return Task.CompletedTask;
    }

    public virtual Task Delete(T record)
    {
        dbContext.Remove(record);

        return Task.CompletedTask;
    }

    public virtual async Task<T?> GetByIdOrDefaultAsync(int id)
    {
        var result = await dbContext.FindAsync<T>(id);

        return result;
    }

    public virtual Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
