using System.Linq.Expressions;
using Altin.Core;
using Microsoft.EntityFrameworkCore;

namespace Altin.DataAccess;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TEntity> dbSet;

    public BaseRepository(DatabaseContext context)
    {
        _context = context;
        dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await dbSet.AddAsync(entity)).Entity;
        await _context.SaveChangesAsync();

        return addedEntity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = dbSet.Remove(entity).Entity;
        await _context.SaveChangesAsync();

        return removedEntity;
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await dbSet.Where(predicate).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return await dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
