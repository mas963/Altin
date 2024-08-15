using Altin.Core.Entities;

namespace Altin.DataAccess.Repositories.Impl;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task AddCategoryProductAsync(Guid categoryId, Guid productId)
    {
        await _context.CategoryProducts.AddAsync(new CategoryProduct
        {
            CategoryId = categoryId,
            ProductId = productId
        });
        await _context.SaveChangesAsync();
    }
}