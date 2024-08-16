using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Guid>> GetCategoryIdToProductAsync(Guid productId);
    Task AddCategoryToProductAsync(Guid categoryId, Guid productId);
    Task DeleteAllCategoryToProductAsync(Guid productId);
}