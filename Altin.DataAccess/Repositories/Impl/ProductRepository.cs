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

    public async Task<List<Product>> GetSimilarProductsAsync(Guid productId)
    {
        var productCategories = await _context.Products
            .Where(p => p.Id == productId)
            .Include(p => p.CategoryProducts) // Ürünün CategoryProducts tablosu ile olan ilişkisini dahil et
            .ThenInclude(cp => cp.Category) // CategoryProducts üzerinden Category'leri dahil et
            .SelectMany(p => p.CategoryProducts.Select(cp => cp.Category)) // Kategorileri seç
            .ToListAsync();

        // Benzer ürünleri filtrele ve eşleşen kategori sayısına göre sırala
        var similarProducts = await _context.Products
            .Where(p => p.Id != productId) // Aynı ürünü dahil etme
            .Select(p => new
            {
                Product = p,
                MatchCount =
                    p.CategoryProducts.Count(cp =>
                        productCategories.Contains(cp.Category)) // Eşleşen kategori sayısını hesapla
            })
            .Where(p => p.MatchCount > 0) // Eşleşmesi olanları al
            .OrderByDescending(p => p.MatchCount) // Eşleşme sayısına göre sırala (çoktan aza)
            .Select(p => p.Product) // Sadece ürünü al
            .Take(8) // En fazla 8 ürün getir
            .ToListAsync();

        return similarProducts;
    }
}