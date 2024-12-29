using Altin.Application.Exceptions;
using Altin.Application.Models.Product;
using Altin.Core.Entities;
using Altin.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Altin.Application.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;
    
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,
        IMemoryCache memoryCache)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }

    public async Task<GetProductModel> GetAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        return new GetProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Slug = product.Slug,
            ImageUrl = product.ImageUrl
        };
    }

    public async Task<ProductUpdateModel> GetUpdateDetailAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        return new ProductUpdateModel
        {
            Id = product.Id,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductImageUrl = product.ImageUrl,
            HepsiburadaUrl = product.HepsiburadaUrl,
            TrendyolUrl = product.TrendyolUrl,
            Price = product.Price,
        };
    }

    public async Task<GetProductDetailModel> GetBySlugAsync(string slug)
    {
        var product = await _productRepository.GetAsync(x => x.Slug == slug,
            include: x => x.Include(x => x.CategoryProducts).ThenInclude(x => x.Category));

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        return new GetProductDetailModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            HepsiBuradaUrl = product.HepsiburadaUrl,
            TrendyolUrl = product.TrendyolUrl,
            Price = product.Price,
            IsPopular = product.IsPopular,
            Categories = product.CategoryProducts.Select(x => x.Category.Name).ToList()
        };
    }

    public async Task<List<GetProductModel>> GetPopularProductsAsync()
    {
        var cacheKey = "popular_products";
        
        if (!_memoryCache.TryGetValue(cacheKey, out List<GetProductModel> productList))
        {
            var products = await _productRepository.GetAllAsync(x => x.IsPopular);

            productList = products.Select(x => new GetProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Slug = x.Slug,
                ImageUrl = x.ImageUrl
            }).ToList();
            
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(5)); // Absolute expiration

            _memoryCache.Set(cacheKey, productList, cacheOptions);
        }
        
        return productList;
    }

    public async Task<GetProductWithPagination> GetAllAsync(int page = 1, int size = 9)
    {
        int totalProduct = await _productRepository.CountAsync();
        var products = await _productRepository.GetAllWithPaginationAsync(page, size);

        var result = new GetProductWithPagination
        {
            Products = products.Select(x => new GetProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Slug = x.Slug,
                ImageUrl = x.ImageUrl
            }).ToList(),
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalProduct / (double)size)
        };

        return result;
    }

    public async Task<GetProductWithPagination> GetAllByCategoryAsync(string categoryNormalizeName, int page = 1,
        int size = 9)
    {
        var totalProduct = await _productRepository.CountByCategoryAsync(categoryNormalizeName);
        var products = await _productRepository.GetAllByCategoryWithPaginationAsync(categoryNormalizeName, page, size);
        var category = await _categoryRepository.GetAsync(x => x.NormalizeName == categoryNormalizeName);

        if (products.Count == 0)
        {
            throw new NotFoundException("Kategori bulunamadı");
        }

        var result = new GetProductWithPagination
        {
            Products = products.Select(x => new GetProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            }).ToList(),

            CategoryName = category.Name,
            CategoryNormalizeName = category.NormalizeName,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalProduct / (double)size)
        };

        return result;
    }

    public async Task AddAsync(ProductUploadModel model)
    {
        var product = new Product
        {
            Name = model.ProductName,
            Description = model.ProductDescription,
            ImageUrl = model.ProductImageName,
            HepsiburadaUrl = model.HepsiburadaUrl,
            TrendyolUrl = model.TrendyolUrl,
            Price = model.Price,
            IsPopular = model.IsPopularProduct,
        };

        if (model.IsPopularProduct)
        {
            var popularProducts = await _productRepository.CountAsync(x => x.IsPopular);

            if (popularProducts >= 8)
            {
                throw new BadRequestException("En fazla 8 adet popüler ürün ekleyebilirsiniz");
            }
            
            _memoryCache.Remove("popular_products");
        }

        await _productRepository.AddAsync(product);

        if (model.CategoryIds != null)
        {
            foreach (var categoryId in model.CategoryIds)
            {
                await _categoryRepository.AddCategoryToProductAsync(categoryId, product.Id);
            }
        }
    }

    public async Task UpdateAsync(ProductUpdateReq model)
    {
        var product = await _productRepository.GetAsync(x => x.Id == model.Id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        product.Name = model.ProductName;
        product.Description = model.ProductDescription;
        product.HepsiburadaUrl = model.HepsiburadaUrl;
        product.TrendyolUrl = model.TrendyolUrl;
        product.Price = model.Price;

        await _productRepository.UpdateAsync(product);

        await _categoryRepository.DeleteAllCategoryToProductAsync(product.Id);

        foreach (var categoryId in model.CategoryIds)
        {
            await _categoryRepository.AddCategoryToProductAsync(categoryId, product.Id);
        }
    }

    public async Task<ProductImageUpdateReturnModel> UpdateImageAsync(Guid id, string imageUrl)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        var oldImageName = product.ImageUrl;
        product.ImageUrl = imageUrl;

        await _productRepository.UpdateAsync(product);
        
        if (product.IsPopular)
            _memoryCache.Remove("popular_products");

        return new ProductImageUpdateReturnModel
        {
            OldImageName = oldImageName
        };
    }

    public async Task<ProductDeleteReturnModel> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetAsync(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        await _productRepository.DeleteAsync(product);
        
        if (product.IsPopular)
            _memoryCache.Remove("popular_products");

        return new ProductDeleteReturnModel
        {
            ProductImage = product.ImageUrl
        };
    }

    public async Task<List<GetProductModel>> GetSimilarProductsAsync(Guid productId)
    {
        var products = await _productRepository.GetSimilarProductsAsync(productId);

        return products.Select(x => new GetProductModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Slug = x.Slug,
            ImageUrl = x.ImageUrl
        }).ToList();
    }
}