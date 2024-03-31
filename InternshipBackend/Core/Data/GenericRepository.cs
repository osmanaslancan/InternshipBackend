using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Core.Data;

public abstract class GenericRepository<T>(InternshipDbContext dbContext) : 
    BaseRepository, IGenericRepository<T>, IRepository
    where T : class, IHasIdField
{
    protected InternshipDbContext DbContext => dbContext;

    public virtual async Task<T> CreateAsync(T record, bool save = true)
    {
        var entry = await DbContext.Set<T>().AddAsync(record);
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
        
        return entry.Entity;
    }

    public virtual async Task<T> UpdateAsync(T record, bool save = true)
    {
        var result = record;
        if (dbContext.Set<T>().Local.All(e => e != record))
        {
            dbContext.Set<T>().Attach(record);
            result = dbContext.Update(record).Entity;
        }

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }

        return result;
    }

    public virtual async Task<T> DeleteAsync(T record, bool save = true)
    {
        var result = DbContext.Set<T>().Remove(record).Entity;

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }

        return result;
    }
    
    public virtual async Task<T?> GetByIdOrDefaultAsync(int id, bool changeTracking = true)
    {
        var queryable = DbContext.Set<T>().AsQueryable();

        if (!changeTracking)
        {
            queryable = queryable.AsNoTracking();
        }

        var result = await queryable.FirstOrDefaultAsync(x => x.Id.Equals(id));

        return result;
    }

    public virtual Task SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }
}
