using InternshipBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace InternshipBackend.Core.Data;

public abstract class GenericRepository<T>(InternshipDbContext dbContext) : 
    BaseRepository, IGenericRepository<T>, IRepository
    where T : class, IHasIdField
{
    protected InternshipDbContext DbContext => dbContext;

    public virtual async Task CreateAsync(T record, bool save = true)
    {
        await DbContext.Set<T>().AddAsync(record);
        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public virtual async Task UpdateAsync(T record, bool save = true)
    {
        if (dbContext.Set<T>().Local.All(e => e != record))
        {
            dbContext.Set<T>().Attach(record);
            dbContext.Update(record);
        }

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(T record, bool save = true)
    {
        DbContext.Set<T>().Remove(record);

        if (save)
        {
            await DbContext.SaveChangesAsync();
        }
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
