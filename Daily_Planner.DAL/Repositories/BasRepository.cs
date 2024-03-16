using Daily_Planner.Domain.Interfaces.Repositories;

namespace Daily_Planner.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext bdContext)
    {
        _dbContext = bdContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new Exception("Entity is null");
        }

        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new Exception("Entity is null");
        }

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        return Task.FromResult(entity);
    }

    public Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new Exception("Entity is null");
        }

        _dbContext.Remove(entity);
        _dbContext.SaveChanges();
        return Task.FromResult(entity);
    }
}