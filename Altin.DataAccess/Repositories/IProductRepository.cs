using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetAllWithPaginationAsync(int page, int size);

    Task<List<Product>> GetAllByCategoryWithPaginationAsync(string categoryNormalizeName, int page,
        int size);

    Task<int> CountByCategoryAsync(string categoryNormalizeName);
}