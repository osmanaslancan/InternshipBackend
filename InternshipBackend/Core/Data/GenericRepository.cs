using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Core.Data;

public abstract class GenericRepository<T>(InternshipDbContext dbContext) : 
    BaseRepository, IGenericRepository<T>, IRepository
    where T : class
{
    protected InternshipDbContext DbContext => dbContext;

    public virtual async Task CreateAsync(T record, bool save = true)
    {
        await DbContext.AddAsync(record);
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public virtual async Task UpdateAsync(T record, bool save = true)
    {
        DbContext.Update(record);

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(T record, bool save = true)
    {
        DbContext.Remove(record);

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
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
