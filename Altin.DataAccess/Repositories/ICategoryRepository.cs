using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task AddCategoryProductAsync(Guid categoryId, Guid productId);
}