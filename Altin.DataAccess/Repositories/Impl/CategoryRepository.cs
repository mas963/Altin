using Altin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Altin.DataAccess.Repositories.Impl;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<List<Guid>> GetCategoryIdToProductAsync(Guid productId)
    {
        var categories = await _context.CategoryProducts
            .Where(x => x.ProductId == productId)
            .Select(x => x.Category)
            .ToListAsync();
        
        return categories.Select(x => x.Id).ToList();
    }

    public async Task AddCategoryToProductAsync(Guid categoryId, Guid productId)
    {
        await _context.CategoryProducts.AddAsync(new CategoryProduct
        {
            CategoryId = categoryId,
            ProductId = productId
        });
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAllCategoryToProductAsync(Guid productId)
    {
         var categories = await _context.CategoryProducts
            .Where(x => x.ProductId == productId)
            .ToListAsync();
         
        _context.CategoryProducts.RemoveRange(categories);
       
        await _context.SaveChangesAsync();
    }
}