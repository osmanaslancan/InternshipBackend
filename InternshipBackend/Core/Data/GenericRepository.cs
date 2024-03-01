using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Core.Data;

public abstract class GenericRepository<T>(InternshipDbContext dbContext) : 
    BaseRepository, IGenericRepository<T>, IRepository
    where T : class
{
    protected InternshipDbContext DbContext => dbContext;

    public virtual async Task CreateAsync(T record)
    {
        await DbContext.AddAsync(record);
    }

    public virtual Task UpdateAsync(T record)
    {
        DbContext.Update(record);

        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T record)
    {
        DbContext.Remove(record);

        return Task.CompletedTask;
    }

    public virtual async Task<T?> GetByIdOrDefaultAsync(object id)
    {
        var result = await DbContext.FindAsync<T>(id);

        return result;
    }

    public virtual Task SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }
}
