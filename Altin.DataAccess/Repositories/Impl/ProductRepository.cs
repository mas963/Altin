using Altin.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Altin.DataAccess.Repositories.Impl;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetAllWithPaginationAsync(int page, int size)
    {
        return await _context.Products
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task<List<Product>> GetAllByCategoryWithPaginationAsync(string categoryNormalizeName, int page,
        int size)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.NormalizeName == categoryNormalizeName);

        if (category == null)
        {
            return new List<Product>();
        }

        var products = await _context.CategoryProducts
            .Where(x => x.CategoryId == category.Id)
            .Include(x => x.Product)
            .Select(x => x.Product)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return products;
    }

    public async Task<int> CountByCategoryAsync(string categoryNormalizeName)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.NormalizeName == categoryNormalizeName);

        if (category == null)
        {
            return 0;
        }

        var productCount = await _context.CategoryProducts
            .Where(x => x.CategoryId == category.Id)
            .CountAsync();

        return productCount;
    }
}