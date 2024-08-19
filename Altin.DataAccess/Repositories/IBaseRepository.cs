using System.Linq.Expressions;
using Altin.Core;

namespace Altin.DataAccess;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
        Expression<Func<TEntity, object>> orderBy = null, bool descending = false);

    Task<TEntity> AddAsync(TEntity entity);

    Task<int> UpdateAsync(TEntity entity);

    Task<int> DeleteAsync(TEntity entity);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
}